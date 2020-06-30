using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.NetworkInformation;
using UnityEngine.UI;


namespace PPEN
{
    public class MySceneController : MonoBehaviour
    {

        [SerializeField]
        private Text wifiAlertText;

        [SerializeField]
        private InputField macaddressInputField;



        // Start is called before the first frame update
        void Start()
        {
            this.macaddressInputField.text = GetWIFIPhysicalAddressListStg();
        }

        // Update is called once per frame
        void Update()
        {
            if(IsUpWIFI() == false)
            {
                //WIFIが有効でない場合
                this.wifiAlertText.enabled = true;
            }
            else
            {
                this.wifiAlertText.enabled = false;
            }
        
        }

        public void TouchedUpdateMacAddress()
        {
            this.macaddressInputField.text = GetWIFIPhysicalAddressListStg();
        }
    



        public void TouchedCopyClipboard()
        {
            GUIUtility.systemCopyBuffer = GetWIFIPhysicalAddressListStg();
        }







        //参考：https://araramistudio.jimdo.com/2019/05/20/c-%E3%81%A7mac%E3%82%A2%E3%83%89%E3%83%AC%E3%82%B9%E3%82%92%E5%8F%96%E5%BE%97%E3%81%99%E3%82%8B/
        public string GetWIFIPhysicalAddressListStg()
        {
            var reText = "";
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var adapter in interfaces)
            {


                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    if ((NetworkInterfaceType.Ethernet == adapter.NetworkInterfaceType))
                    {
                        //有線LAN
                        //reText += "状態UP：Ethernet：Mac Address = " + adapter.GetPhysicalAddress() + "\n";

                    }

                    if ((NetworkInterfaceType.Wireless80211 == adapter.NetworkInterfaceType))
                    {
                        //WIFI
                        // reText += "状態UP：WIFI：Mac Address = " + adapter.GetPhysicalAddress() + "\n";
                        return "" + adapter.GetPhysicalAddress();

                    }
                }
              //  Debug.Log(adapter.Name);

            }
            return reText;
        }


        /// <summary>
        /// WIFIが接続されているか？
        /// </summary>
        /// <returns></returns>
        public bool IsUpWIFI()
        {

            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var adapter in interfaces)
            {

                if (adapter.OperationalStatus == OperationalStatus.Up
                    && adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    return true;
                }



            }
            return false;
        }







    }
}