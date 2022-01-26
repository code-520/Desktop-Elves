using System.Collections;
using System.Collections.Generic;
using UnityEngine;
delegate void SayAll();
public class RoleManger : MonoBehaviour
{
    public static Datas.Emotions emotions;
    public static AudioSource saying;
    public static Animator animator;
    
    private SayAll sayAll;
    private Datas.Welcome welcome;
    private bool rejection;
    private void Awake()
    {
        sayAll = new SayAll(body.SayInBody);
        sayAll += new SayAll(Legs.SayInLegs);
        sayAll += new SayAll(Head.SayInHead);
        //fs = new FileStream("C:\\Users\\86139\\Desktop\\data1.txt", FileMode.Open);
        saying = GetComponent<AudioSource>();
        saying.clip = Resources.Load<AudioClip>("Audio\\Keli\\Self-introduction");
        animator = GetComponent<Animator>();
        rejection = true;
        welcome.Init();
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        emotions = Datas.Emotions.Init();
        StartCoroutine("IsWelcoming");
        StartCoroutine("ChangeStatus");
    }

    private void Update()
    {
        Rotation();
        Quiting();
        if(Legs.isWalking)
        {
            StartCoroutine("Walking");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(transform.localPosition.x>0)
            {
                transform.Translate(Vector3.right * 2);
            }
            else
            {
                transform.Translate(Vector3.left * 2);
            }
        }
    }
    //程序开始时的效果实现
    private IEnumerator IsWelcoming()
    {
        float size = 0f;
        while(size<=7)
        {
            if(size>1.1f&&size<1.3f)
            {
                saying.Play();
            }
            size += 0.1f;
            transform.localScale = new Vector3(size, size, size);
            yield return new WaitForSeconds(0.05f);
        }
        animator.SetBool("normal", true);
    }
    
    //角色旋转
    private void Rotation()
    {
        //获取鼠标滚轮的值
        float yAngle = Input.GetAxis("Mouse ScrollWheel");
        //解决90度和270度附近无法旋转的问题
        Quaternion nowAngle = transform.localRotation;
        
        
        if (yAngle != 0&&WindowManager.isInRoleRect)
        {
            transform.Rotate(0, yAngle * 90, 0);
        }
    }
    
    //角色拖拽
    private IEnumerator OnMouseDown()
    {
        int cnt = 0;
        //1. 得到物体的屏幕坐标
        Vector3 cubeScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        //2. 计算偏移量
        //鼠标的三维坐标
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
        //鼠标三维坐标转为世界坐标
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 offset = transform.position - mousePos;

        //3. 物体随着鼠标移动
        while (Input.GetMouseButton(0))
        {
            cnt++;
            //目前的鼠标二维坐标转为三维坐标
            Vector3 curMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
            //目前的鼠标三维坐标转为世界坐标
            curMousePos = Camera.main.ScreenToWorldPoint(curMousePos);

            //物体世界位置
            transform.position = curMousePos + offset;
            yield return new WaitForFixedUpdate(); //这个很重要，循环执行
        }
        //判断是否是单击
        if (cnt <= 10)
        {
            sayAll();
        }
    }

    //走动
    private IEnumerator Walking()
    {
        Quaternion q = transform.localRotation;
        Debug.Log(q.x);
        Debug.Log(q.y);
        Debug.Log(q.z);
        Debug.Log(q.w);
        Legs.isWalking = false;
        float speed = transform.localScale.x/30.0f;
        float distanceMax = speed * 20.0f;
        Vector3 pos = transform.localPosition;
        bool directRight = false;
        if(pos.x>0)
        {
            directRight = true;
            transform.localRotation = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        animator.SetTrigger("walk");
        float distance = 0.0f;
        while (distance <= distanceMax)
        {
            if (directRight)
            {
                transform.localPosition = new Vector3(pos.x - distance, pos.y, pos.z);
            }
            else
            {
                transform.localPosition = new Vector3(pos.x + distance, pos.y, pos.z);
            }
            distance += speed;
            yield return new WaitForSeconds(0.05f);
        }
        animator.SetTrigger("notwalk");
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        StopCoroutine("Walking");
    }
    //退出
    private void Quiting()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(System.DateTime.Now.Hour==16&&rejection)
                {
                    rejection = false;
                    saying.Stop();
                    saying.clip = Resources.Load<AudioClip>("Audio\\Keli\\Rejection");
                    saying.Play();
                    StartCoroutine("RejectionAnimation");
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }
    //拒绝退出动画
    private IEnumerator RejectionAnimation()
    {
        yield return new WaitForSeconds(9f);
        animator.SetTrigger("rejection");
    }

    //
    private IEnumerator ChangeStatus()
    {
        int time;
        int cnt;
        string name;
        while(true)
        {
            time = Random.Range(10, 30);
            yield return new WaitForSeconds(time);
            cnt = Random.Range(1, 4);
            name = string.Format("normal{0}", cnt);
            animator.SetTrigger(name);
        }
    }
}
