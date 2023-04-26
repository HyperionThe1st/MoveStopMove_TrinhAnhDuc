using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name : MonoBehaviour
{
    public static Name instanceName;

    private string[] FIRSTNAME = { "Trinh", "Nguyen", "Vu", "Do", "Hoang", "Pham", "Tran" };
    private string[] SECONDNAME = { "Van", "Anh", "Thi" };
    private string[] LASTNAME = { "Duc", "Manh", "Son", "Hung", "Vinh", "Quoc" };
    private void Awake()
    {
        instanceName = this;
    }

    private void Start()
    {
        
    }
    public string RandomName()
    {
        string name = "";
        int first_name = Random.Range(0, FIRSTNAME.Length);
        int second_name = Random.Range(0, SECONDNAME.Length);
        int last_name = Random.Range(0, LASTNAME.Length);
        name += FIRSTNAME[first_name] + SECONDNAME[second_name] + LASTNAME[last_name];
        return name;
    }





}
