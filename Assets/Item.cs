using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	public GameObject sphere;
	public Vector3 offset;
	public KartController Kart;
	public int itemNo;
	public float boost;
	public float amount;
	public Transform front, back;
	public GameObject greenShell;
	public GameObject shell;
    public GameObject banana;
	public float shellSpeed;
	public Image itemUIImage;

	[Header("Item UIs")]
	public Sprite mush1;
	public Sprite mush2;
	public Sprite mush3;
	public Sprite shell1;
	public Sprite shell2;
	public Sprite shell3;
    public Sprite banana1;
    public Sprite banana2;
    public Sprite banana3;


    void Awake()
	{
		Kart = GetComponentInParent<KartController>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (sphere.transform.position - offset);//Follow sphere

		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButtonDown("Left Shoulder"))
		{
			UseItem();
		}
    }

	public void UpdateUI()
	{
        itemUIImage.color = Color.white;
        if (itemNo == 0)
        {
            itemUIImage.sprite = null;
            itemUIImage.color = Color.black;
        }
        else if (itemNo == 1) //Single Mushroom
        {
            itemUIImage.sprite = mush1;
        }
        else if (itemNo == 2) // Double Mushroom
        {
            itemUIImage.sprite = mush2;
        }
        else if (itemNo == 3) //Triple Mushroom
        {
            itemUIImage.sprite = mush3;
        }
        else if (itemNo == 4) //Single Green Shell
        {
            itemUIImage.sprite = shell1;
        }
        else if (itemNo == 5) //Double Green Shell
        {
            itemUIImage.sprite = shell2;
        }
        else if (itemNo == 6) //Triple Green Shell
        {
            itemUIImage.sprite = shell3;
        }
        else if (itemNo == 7) //Single banana
        {
            itemUIImage.sprite = banana1;
        }
        else if (itemNo == 8) //Double banana
        {
            itemUIImage.sprite = banana2;
        }
        else if (itemNo == 9) //Triple banana
        {
            itemUIImage.sprite = banana3;
        }
    }

	void UseItem()
	{
		if(itemNo == 1) //Single Mushroom
		{
            Kart.StartBoost(amount, boost);
			itemNo = 0;
            UpdateUI();
        }
		if(itemNo == 2) // Double Mushroom
		{
			Kart.StartBoost(amount, boost);
			itemNo = 1;
            UpdateUI();
        }
        if (itemNo == 3) //Triple Mushroom
		{
            Kart.StartBoost(amount, boost);
            itemNo = 2;
            UpdateUI();
        }
        if (itemNo == 4) //Single Green Shell
		{
			shell = Instantiate(greenShell, front.position, Quaternion.identity);
			shell.GetComponent<Rigidbody>().AddForce(front.forward * shellSpeed, ForceMode.Impulse);
			itemNo = 0;
            UpdateUI();
        }
        if (itemNo == 5) //Double Green Shell
        {
            shell = Instantiate(greenShell, front.position, Quaternion.identity);
            shell.GetComponent<Rigidbody>().AddForce(front.forward * shellSpeed, ForceMode.Impulse);
            itemNo = 4;
            UpdateUI();
        }
        if (itemNo == 6) //Triple Green Shell
        {
            shell = Instantiate(greenShell, front.position, Quaternion.identity);
            shell.GetComponent<Rigidbody>().AddForce(front.forward * shellSpeed, ForceMode.Impulse);
            itemNo = 5;
            UpdateUI();
        }
        if (itemNo == 7) //Single banana
        {
            Instantiate(banana, back.position, Quaternion.identity);
            itemNo = 0;
            UpdateUI();
        }
        if (itemNo == 8) //Double banana
        {
            Instantiate(banana, back.position, Quaternion.identity);
            itemNo = 7;
            UpdateUI();
        }
        if (itemNo == 9) //Triple banana
        {
            Instantiate(banana, back.position, Quaternion.identity);
            itemNo = 8;
            UpdateUI();
        }
    }
}
