using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace BooDongSan
{
    public class Apicon
    {
        #region api urls
        /*
const string URL1 = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcShTrade"; //단독다가구 매매
const string URL2 = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcSHRent"; //단독다가구 전월세
const string URL3 = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcOffiTrade"; //오피스텔 매매
const string URL4 = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcOffiRent"; //오피스텔 전월세
const string URL5 = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcAptTradeDev"; //아파트 매매
const string URL6 = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcAptRent"; //아파트 전월세
const string URL7 = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcSilvTrade"; //아파트 분양권
const string URL8 = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcNrgTrade"; //상업업무용
const string URL9 = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcRhTrade"; //연립다세대 매매
const string URL10 = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcRHRent"; //연립다세대 전월세
const string URL11 = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcLandTrade"; //토지
*/
        #endregion

        #region 지역코드
        /*        
        string seoulCode = "11110"; //서울
        string gyeongCode = "64100"; //경기도
        string gangCode = "64200"; //강원도
        string cbCode = "64300"; //충청북도
        string cnCode = "64400"; //충청남도
        string gbCode = "64700"; //경상북도
        string gnCode = "64800"; //경상남도
        string jbCode = "64500"; //전라북도
        string jnCode = "64600"; //전라남도
        string jjCode = "65000"; //제주도
        string incheonCode = "62800"; //인천광역시
        string daeCode = "63000"; //대전광역시
        string daegooCode = "62700"; //대구광역시
        string woolCode = "63100"; //울산광역시
        string busanCode = "62600"; //부산광역시
        string gwangCode = "62900"; //광주광역시
        */
        #endregion

        string today = "&DEAL_YMD=" + DateTime.Now.ToString("yyyyMM");
        #region 자료구조
        public class Colums
        {
            public string Local { get; set; } //지역
            public string Bunji { get; set; } //번지
            public string DY { get; set; } //계약년도
            public string DM { get; set; } //계약월
            public string DD { get; set; } //계약일
            public string BY { get; set; } //건축년도
            public string Area { get; set; } //면적
            public string AN { get; set; } //아파트 이름
            public string CDD { get; set; } //해체사유발생일
            public int Deposit { get; set; } //보증금
            public int Floor { get; set; } //층
        }
        List<Colums> colums = new List<Colums>();
        #endregion
        public void getResults()
        {
            #region api url
            string[] urls = new string[11];

            urls[0] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcSHTrade?_wadl&type=xml"; //단독다가구 매매
            urls[1] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcSHRent?_wadl&type=xml"; //단독다가구 전월세
            urls[2] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcOffiTrade?_wadl&type=xml"; //오피스텔 매매
            urls[3] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcOffiRent?_wadl&type=xml"; //오피스텔 전월세
            urls[4] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcAptTradeDev?_wadl&type=xml"; //아파트 매매
            urls[5] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcAptRent?_wadl&type=xml"; //아파트 전월세
            urls[6] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcSilvTrade?_wadl&type=xml"; //아파트 분양권
            urls[7] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcNrgTrade?_wadl&type=xml"; //상업업무용
            urls[8] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcRhTrade?_wadl&type=xml"; //연립다세대 매매
            urls[9] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcRHRent?_wadl&type=xml"; //연립다세대 전월세
            urls[10] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcLandTrade?_wadl&type=xml"; //토지
            #endregion

            #region 지역코드 리스트
            string[] codes = new string[16];
            codes[0] = "11110"; //서울
            codes[1] = "64100"; //경기도
            codes[2] = "64200"; //강원도
            codes[3] = "64300"; //충청북도
            codes[4] = "64400"; //충청남도
            codes[5] = "64700"; //경상북도
            codes[6] = "64800"; //경상남도
            codes[7] = "64500"; //전라북도
            codes[8] = "64600"; //전라남도
            codes[9] = "65000"; //제주도
            codes[10] = "62800"; //인천광역시
            codes[11] = "63000"; //대전광역시
            codes[12] = "62700"; //대구광역시
            codes[13] = "63100"; //울산광역시
            codes[14] = "62600"; //부산광역시
            codes[15] = "62900"; //광주광역시
            #endregion
            string serviceKey = "7LxnnA3%2B7VG88HLozXe%2BwxvC8dB58arnn4YM3mhcgmQcWXXsM4FY8ZS34MOyZieNoNwDBOeySlqV9YHjyMeMhA%3D%3D";
            serviceKey = "&serviceKey=" + HttpUtility.UrlEncode(serviceKey, Encoding.GetEncoding("UTF-8"));
            string search = "";

            for (int i = 0; i < urls.Length; i++)
            {
                for (int j = 0; j < codes.Length; j++)
                {
                    search = "?LAWD_CD=" + codes[j] + "&DEAL_YMD=202110";
                    WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
                    WebRequest wrq = WebRequest.Create(urls[i] + search + serviceKey);
                    wrq.Method = "GET";

                    WebResponse wrs = wrq.GetResponse();
                    Stream s = wrs.GetResponseStream();
                    StreamReader sr = new StreamReader(s);

                    string response = sr.ReadToEnd();

                    //데이터 분류
                    XmlDocument xd = new XmlDocument();
                    xd.LoadXml(response);
                    XmlNode xn = xd["response"]["body"]["items"];
                }
            }
            //int today = Convert.ToInt32(xn.ChildNodes[0]["decideCnt"].InnerText);
            //int yday = Convert.ToInt32(xn.ChildNodes[1]["decideCnt"].InnerText);
            //int today_decide = today - yday;
            //this.label5.Text = xn.ChildNodes[0]["decideCnt"].InnerText;
            //this.label6.Text = today_decide.ToString();
            //this.label7.Text = xn.ChildNodes[0]["clearCnt"].InnerText;
            //this.label8.Text = xn.ChildNodes[0]["deathCnt"].InnerText;
            //this.label10.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}