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
    //����ʼʱ��Ч��ʵ��
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
    
    //��ɫ��ת
    private void Rotation()
    {
        //��ȡ�����ֵ�ֵ
        float yAngle = Input.GetAxis("Mouse ScrollWheel");
        //���90�Ⱥ�270�ȸ����޷���ת������
        Quaternion nowAngle = transform.localRotation;
        
        
        if (yAngle != 0&&WindowManager.isInRoleRect)
        {
            transform.Rotate(0, yAngle * 90, 0);
        }
    }
    
    //��ɫ��ק
    private IEnumerator OnMouseDown()
    {
        int cnt = 0;
        //1. �õ��������Ļ����
        Vector3 cubeScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        //2. ����ƫ����
        //������ά����
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
        //�����ά����תΪ��������
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 offset = transform.position - mousePos;

        //3. ������������ƶ�
        while (Input.GetMouseButton(0))
        {
            cnt++;
            //Ŀǰ������ά����תΪ��ά����
            Vector3 curMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
            //Ŀǰ�������ά����תΪ��������
            curMousePos = Camera.main.ScreenToWorldPoint(curMousePos);

            //��������λ��
            transform.position = curMousePos + offset;
            yield return new WaitForFixedUpdate(); //�������Ҫ��ѭ��ִ��
        }
        //�ж��Ƿ��ǵ���
        if (cnt <= 10)
        {
            sayAll();
        }
    }

    //�߶�
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
    //�˳�
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
    //�ܾ��˳�����
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
