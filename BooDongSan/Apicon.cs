using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

        string today = "&DEAL_YMD=" + DateTime.Now.ToString("yyyyMM");

        #region 자료구조
        public class Colums
        {
            public string Local { get; set; } //지역
            public string Bunji { get; set; } //번지
            public string Bon { get; set; } //본번
            public string Bu { get; set; } //부번
            public string Doro { get; set; } //도로명
            public string DY { get; set; } //계약년도
            public string DM { get; set; } //계약월
            public string DD { get; set; } //계약일
            public string DT { get; set; } //전월세 구분
            public string BY { get; set; } //건축년도
            public string BT { get; set; } //주택유형
            public string Bub { get; set; } //법정동
            public string Area { get; set; } //대지면적
            public string AreaY { get; set; } //연면적
            public string AreaD { get; set; } //계약면적
            public string AreaE { get; set; } //전용면적
            public string Address1 { get; set; } //지역(도, 특별시, 특례시, 광역시)
            public string Address2 { get; set; } //지역(시군구)
            public string AN { get; set; } //아파트 이름
            public string CDD { get; set; } //해체사유발생일
            public string Deposit { get; set; } //보증금, 거래금액
            public string MR { get; set; } //월세
            public string Floor { get; set; } //층
            public string Ownership { get; set; } //분양권/입주권 구분
            public string BuildU { get; set; } //건물주용도
            public string LandU { get; set; } //용도지역
        }
        List<Colums> colums = new List<Colums>();
        #endregion

        /// <summary>
        /// API 데이터 가져오기
        /// </summary>
        public void getResults()
        {
            #region api url
            string[] urls = new string[11];

            urls[0] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcSHTrade"; //단독다가구 매매
            urls[1] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcSHRent"; //단독다가구 전월세
            urls[2] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcOffiTrade"; //오피스텔 매매
            urls[3] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcOffiRent"; //오피스텔 전월세
            urls[4] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcAptTradeDev"; //아파트 매매
            urls[5] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcAptRent"; //아파트 전월세
            urls[6] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcSilvTradel"; //아파트 분양권
            urls[7] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcNrgTrade"; //상업업무용
            urls[8] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcRhTrade"; //연립다세대 매매
            urls[9] = "http://openapi.molit.go.kr:8081/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcRHRent"; //연립다세대 전월세
            urls[10] = "http://openapi.molit.go.kr/OpenAPI_ToolInstallPackage/service/rest/RTMSOBJSvc/getRTMSDataSvcLandTrade"; //토지
            #endregion

            #region 지역코드 리스트
            string[] codes = new string[16];
            codes[0] = "11000!@#서울특별시"; //서울
            codes[1] = "64100!@#경기도"; //경기도
            codes[2] = "64200!@#강원도"; //강원도
            codes[3] = "64300!@#충청북도"; //충청북도
            codes[4] = "64400!@#충청남도"; //충청남도
            codes[5] = "64700!@#경상북도"; //경상북도
            codes[6] = "64800!@#경상남도"; //경상남도
            codes[7] = "64500!@#전라북도"; //전라북도
            codes[8] = "64600!@#전라남도"; //전라남도
            codes[9] = "65000!@#제주도"; //제주도
            codes[10] = "62800!@#인천광역시"; //인천광역시
            codes[11] = "63000!@#대전광역시"; //대전광역시
            codes[12] = "62700!@#대구광역시"; //대구광역시
            codes[13] = "63100!@#울산광역시"; //울산광역시
            codes[14] = "62600!@#부산광역시"; //부산광역시
            codes[15] = "62900!@#광주광역시"; //광주광역시
            #endregion

            string serviceKey = "?serviceKey=7LxnnA3%2B7VG88HLozXe%2BwxvC8dB58arnn4YM3mhcgmQcWXXsM4FY8ZS34MOyZieNoNwDBOeySlqV9YHjyMeMhA%3D%3D";
            string search = "";

            for (int i = 0; i < urls.Length; i++) //부동산 거래 항목(주택, 아파트, 오피스텔 등등)
            {
                for (int j = 0; j < codes.Length; j++) //법정동 코드에 따른 분류
                {
                    string[] code = codes[j].Split(new string[] { "!@#" }, StringSplitOptions.None); //법정동 코드와 지역이름 나누기
                    search = "&LAWD_CD=" + code[0] + "&DEAL_YMD=202110";
                    WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
                    WebRequest wrq = WebRequest.Create(urls[i] + serviceKey + search);
                    wrq.Method = "GET";

                    WebResponse wrs = wrq.GetResponse();
                    Stream s = wrs.GetResponseStream();
                    StreamReader sr = new StreamReader(s);

                    string response = sr.ReadToEnd();

                    //데이터 분류
                    XmlDocument xd = new XmlDocument();
                    xd.LoadXml(response);
                    XmlNode xn = xd["response"]["body"]["items"];

                    for(int k = 0; k < xn.ChildNodes.Count; k++) //api 데이터
                    {
                        Colums cl = new Colums();

                        #region 단독 다가구 매매
                        if (i == 0) //단독 다가구 매매
                        {
                            cl.Deposit = xn.ChildNodes[k]["거래금액"].InnerText.Trim();
                            cl.BY = xn.ChildNodes[k]["건축년도"].InnerText.Trim();
                            cl.DY = xn.ChildNodes[k]["년"].InnerText.Trim();
                            cl.DM = xn.ChildNodes[k]["월"].InnerText.Trim();
                            cl.DD = xn.ChildNodes[k]["일"].InnerText.Trim();
                            cl.BT = xn.ChildNodes[k]["주택유형"].InnerText.Trim();
                            cl.Area = xn.ChildNodes[k]["대지면적"].InnerText.Trim();
                            cl.AreaY = xn.ChildNodes[k]["연면적"].InnerText.Trim();
                            cl.Bub = xn.ChildNodes[k]["법정동"].InnerText.Trim();      
                            cl.Address1 = code[1];
                            cl.Address2 = getAddress(cl.Bub); //시군구 가져오기
                        }
                        #endregion

                        #region 단독 다가구 전/월세
                        else if (i == 1) //단독 다가구 전/월세
                        {
                            cl.Deposit = xn.ChildNodes[k]["보증금액"].InnerText.Trim();
                            cl.MR = xn.ChildNodes[k]["월세금액"].InnerText.Trim();
                            if (cl.MR == "0") cl.DT = "전세";
                            else cl.DT = "월세";
                            cl.DY = xn.ChildNodes[k]["년"].InnerText.Trim();
                            cl.DM = xn.ChildNodes[k]["월"].InnerText.Trim();
                            cl.DD = xn.ChildNodes[k]["일"].InnerText.Trim();
                            cl.AreaD = xn.ChildNodes[k]["계약면적"].InnerText.Trim();
                            cl.Bub = xn.ChildNodes[k]["법정동"].InnerText.Trim();
                            cl.Address1 = code[1];
                            cl.Address2 = getAddress(cl.Bub); //시군구 가져오기
                        }
                        #endregion

                        #region 오피스텔 매매
                        else if (i == 2) //오피스텔 매매
                        {
                            cl.Deposit = xn.ChildNodes[k]["거래금액"].InnerText.Trim();
                            cl.MR = xn.ChildNodes[k]["월세"].InnerText.Trim();
                            cl.DY = xn.ChildNodes[k]["년"].InnerText.Trim();
                            cl.DM = xn.ChildNodes[k]["월"].InnerText.Trim();
                            cl.DD = xn.ChildNodes[k]["일"].InnerText.Trim();
                            cl.AreaE = xn.ChildNodes[k]["전용면적"].InnerText.Trim();
                            cl.Bub = xn.ChildNodes[k]["법정동"].InnerText.Trim();
                            cl.Address1 = code[1];
                            cl.Address2 = xn.ChildNodes[k]["시군구"].InnerText.Trim();
                            cl.AN = xn.ChildNodes[k]["단지"].InnerText.Trim();
                            cl.Bunji = xn.ChildNodes[k]["지번"].InnerText.Trim();
                            cl.Floor = xn.ChildNodes[k]["층"].InnerText.Trim();
                            cl.CDD = xn.ChildNodes[k]["해체사유발생일"].InnerText.Trim();
                        }
                        #endregion

                        #region 오피스텔 전/월세
                        else if (i == 3) //오피스텔 전/월세
                        {
                            cl.Deposit = xn.ChildNodes[k]["보증금"].InnerText.Trim();
                            cl.DY = xn.ChildNodes[k]["년"].InnerText.Trim();
                            cl.DM = xn.ChildNodes[k]["월"].InnerText.Trim();
                            cl.DD = xn.ChildNodes[k]["일"].InnerText.Trim();
                            cl.AreaE = xn.ChildNodes[k]["전용면적"].InnerText.Trim();
                            cl.Bub = xn.ChildNodes[k]["법정동"].InnerText.Trim();
                            cl.Address1 = code[1];
                            cl.Address2 = xn.ChildNodes[k]["시군구"].InnerText.Trim();
                            cl.AN = xn.ChildNodes[k]["단지"].InnerText.Trim();
                            cl.Bunji = xn.ChildNodes[k]["지번"].InnerText.Trim();
                            cl.Floor = xn.ChildNodes[k]["층"].InnerText.Trim();
                            cl.CDD = xn.ChildNodes[k]["해체사유발생일"].InnerText.Trim();
                        }
                        #endregion

                        #region 아파트 매매
                        else if (i == 4) //아파트 매매
                        {
                            cl.Deposit = xn.ChildNodes[k]["거래금액"].InnerText.Trim();
                            cl.BY = xn.ChildNodes[k]["건축년도"].InnerText.Trim();
                            cl.DY = xn.ChildNodes[k]["년"].InnerText.Trim();
                            cl.DM = xn.ChildNodes[k]["월"].InnerText.Trim();
                            cl.DD = xn.ChildNodes[k]["일"].InnerText.Trim();
                            cl.AreaE = xn.ChildNodes[k]["전용면적"].InnerText.Trim();
                            cl.Bub = xn.ChildNodes[k]["법정동"].InnerText.Trim();
                            cl.Address1 = code[1];
                            cl.Address2 = getAddress(cl.Bub); //시군구 가져오기
                            cl.AN = xn.ChildNodes[k]["아파트"].InnerText.Trim();
                            cl.Bunji = xn.ChildNodes[k]["지번"].InnerText.Trim();
                            cl.Bon = xn.ChildNodes[k]["법정동본번코드"].InnerText.Trim();
                            cl.Bu = xn.ChildNodes[k]["법정동부번코드"].InnerText.Trim();
                            cl.Doro = xn.ChildNodes[k]["도로명"].InnerText.Trim();
                            cl.Floor = xn.ChildNodes[k]["층"].InnerText.Trim();
                            cl.CDD = xn.ChildNodes[k]["해체사유발생일"].InnerText.Trim();
                        }
                        #endregion

                        #region 아파트 전/월세
                        else if (i == 5) //아파트 전/월세
                        {
                            cl.Deposit = xn.ChildNodes[k]["거래금액"].InnerText.Trim();
                            cl.Ownership = xn.ChildNodes[k]["구분"].InnerText.Trim();
                            cl.DY = xn.ChildNodes[k]["년"].InnerText.Trim();
                            cl.DM = xn.ChildNodes[k]["월"].InnerText.Trim();
                            cl.DD = xn.ChildNodes[k]["일"].InnerText.Trim();
                            cl.AreaE = xn.ChildNodes[k]["전용면적"].InnerText.Trim();
                            cl.Bub = xn.ChildNodes[k]["법정동"].InnerText.Trim();
                            cl.Address1 = code[1]; //16개 시도
                            cl.Address2 = xn.ChildNodes[k]["시군구"].InnerText.Trim();
                            cl.AN = xn.ChildNodes[k]["단지"].InnerText.Trim();
                            cl.Bunji = xn.ChildNodes[k]["지번"].InnerText.Trim();
                            cl.Floor = xn.ChildNodes[k]["층"].InnerText.Trim();
                        }
                        #endregion

                        #region 아파트 분양권
                        else if (i == 6) //아파트 분양권
                        {
                            cl.Deposit = xn.ChildNodes[k]["보증금액"].InnerText.Trim();
                            cl.MR = xn.ChildNodes[k]["월세금액"].InnerText.Trim();
                            if (cl.MR == "0") cl.DT = "전세";
                            else cl.DT = "월세";
                            cl.BY = xn.ChildNodes[k]["건축년도"].InnerText.Trim();
                            cl.DY = xn.ChildNodes[k]["년"].InnerText.Trim();
                            cl.DM = xn.ChildNodes[k]["월"].InnerText.Trim();
                            cl.DD = xn.ChildNodes[k]["일"].InnerText.Trim();
                            cl.AreaE = xn.ChildNodes[k]["전용면적"].InnerText.Trim();
                            cl.Bub = xn.ChildNodes[k]["법정동"].InnerText.Trim();
                            cl.Address1 = code[1]; //16개 시도
                            cl.Address2 = xn.ChildNodes[k]["시군구"].InnerText.Trim();
                            cl.AN = xn.ChildNodes[k]["아파트"].InnerText.Trim();
                            cl.Bunji = xn.ChildNodes[k]["지번"].InnerText.Trim();
                            cl.Doro = xn.ChildNodes[k]["도로명"].InnerText.Trim();
                            cl.Floor = xn.ChildNodes[k]["층"].InnerText.Trim();
                            cl.CDD = xn.ChildNodes[k]["해체사유발생일"].InnerText.Trim();
                        }
                        #endregion

                        #region 상업 업무용
                        else if (i == 7) //상업 업무용
                        {
                            cl.Deposit = xn.ChildNodes[k]["거래금액"].InnerText.Trim();
                            cl.BY = xn.ChildNodes[k]["건축년도"].InnerText.Trim();
                            cl.BuildU = xn.ChildNodes[k]["건물주용도"].InnerText.Trim();
                            cl.LandU = xn.ChildNodes[k]["용도지역"].InnerText.Trim();
                            cl.BT = xn.ChildNodes[k]["유형"].InnerText.Trim();
                            cl.DY = xn.ChildNodes[k]["년"].InnerText.Trim();
                            cl.DM = xn.ChildNodes[k]["월"].InnerText.Trim();
                            cl.DD = xn.ChildNodes[k]["일"].InnerText.Trim();
                            cl.AreaE = xn.ChildNodes[k]["전용면적"].InnerText.Trim();
                            cl.Bub = xn.ChildNodes[k]["법정동"].InnerText.Trim();
                            cl.Address1 = code[1];
                            cl.Address2 = xn.ChildNodes[k]["시군구"].InnerText.Trim();
                            cl.Floor = xn.ChildNodes[k]["층"].InnerText.Trim();
                            cl.CDD = xn.ChildNodes[k]["해체사유발생일"].InnerText.Trim();
                        }
                        #endregion
                        colums.Add(cl);
                    }
                    //이 자리에서 테이블에 인서트
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

        #region 시군구 메서드
        public string getAddress(string bub) //시군구 가져오기
        {
            string address = "";
            string sql = string.Format("SELECT TOP 1 시군구 FROM dbo.읍면동$ WHERE 법정동 = '{0}'", bub);
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFDbcontext"].ConnectionString))
            {
                var command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            address = reader["시군구"].ToString();
                        }
                    }
                }
            }
            return address;
        }
        #endregion
    }
}