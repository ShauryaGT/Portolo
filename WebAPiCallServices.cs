using MAGIApp.Constants;
using MAGIApp.Interface;
using MAGIApp.Model.RequestModel;
using MAGIApp.Model.ResponseModel;
using MAGIApp.Utils;
using MAGIApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MAGIApp.Services
{
    public class WebAPiCallServices
    {
        private readonly IRequestService _requestService;
        private HttpClient HttpClientInstance = new HttpClient();
        public WebAPiCallServices()
        {
            _requestService = new RequestService();
        }
        public async Task<TokenResponseModel> GetUserAuth(TokenRequestModel userauth)
        {
            bool asd = true;
            if (asd)
            {
                try
                {
                    using (HttpClientInstance = new HttpClient())
                    {
                        HttpClientInstance.BaseAddress = new Uri(Constant.GetTokenApiUrl);
                        var postData = new List<KeyValuePair<string, string>>();
                        var dto = new TokenRequestModel
                        {
                            grant_type = userauth.grant_type,
                            password = userauth.password,
                            username = userauth.username
                        };
                        List<KeyValuePair<string, string>> nvc = new List<KeyValuePair<string, string>>();
                        nvc.Add(new KeyValuePair<string, string>("grant_type", userauth.grant_type));
                        nvc.Add(new KeyValuePair<string, string>("username", userauth.username));
                        nvc.Add(new KeyValuePair<string, string>("password", userauth.password));
                        var req = new HttpRequestMessage(HttpMethod.Post, string.Empty) { Content = new FormUrlEncodedContent(nvc) };
                        var res = await HttpClientInstance.SendAsync(req);
                        string result = await res.Content.ReadAsStringAsync();
                        var error = JsonConvert.DeserializeObject<TokenResponseModel>(result);
                        if (res.IsSuccessStatusCode)
                        {
                            var userData = JsonConvert.DeserializeObject<TokenResponseModel>(result);
                            return userData;
                        }
                        else
                        {
                            TokenResponseModel ud = new TokenResponseModel();
                            ud.error_description = error.error_description;
                            return ud;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //if (Util.Expiry < DateTime.Now)
                    //{
                    //    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    //}
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else
            {
                TokenResponseModel ud = new TokenResponseModel();
                return ud;
            }
        }

        public async Task<TokenResponseModel> GetUserProductionAuth(TokenProductionRequestModel userauth)
        {
            Uri uri = new Uri(Constant.SiteURL + "/api/WebAPI/ValidateLogin");
            try
            {
                var json = JsonConvert.SerializeObject(userauth);
                var T_CList = await _requestService.PostAsyncString<TokenResponseModel>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                //if (Util.Expiry < DateTime.Now)
                //{
                //    Application.Current.MainPage = new NavigationPage(new LoginPage());
                //}
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<UserDetailsResponseModel> GetUserDetails()
        {

            Uri uri = new Uri(Constant.BaseUrl + "/UserDetails/" + Util.Userid);
            try
            {
                var UserResult = await _requestService.PostAsyncAPi<UserDetailsResponseModel>(uri);
                return UserResult;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PartnerListResponseModel> GetPartnerList()
        {

            Uri uri = new Uri(Constant.BaseUrl + "/PartnerList/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey);
            try
            {
                var PartnerList = await _requestService.PostAsyncAPi<PartnerListResponseModel>(uri);
                return PartnerList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PartnerGreetingResponseModel> GetGreeting(string EventOrganizations_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/GETGreeting/" + EventOrganizations_pkey);
            try
            {
                var greetinglist = await _requestService.PostAsyncAPi<PartnerGreetingResponseModel>(uri);
                return greetinglist;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<String> UpdateUserList(UList uList)
        {

            Uri uri = new Uri(Constant.BaseUrl + "/UserUpdate");
            try
            {
                var updatemessage = await _requestService.PostAsync<UList, String>(uri, uList);
                return updatemessage;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<OverViewResponseModel> GetOverViewEventList()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/Overview/" + Util.CurrEvent_pkey + "/1");
            try
            {
                var EventList = await _requestService.PostAsyncAPi<OverViewResponseModel>(uri);
                return EventList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ProgramResponseModel> GetProgramList()
        {

            Uri uri = new Uri(Constant.BaseUrl + "/program/" + Util.CurrEvent_pkey);
            try
            {
                var programLists = await _requestService.PostAsyncAPi<ProgramResponseModel>(uri);
                return programLists;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<SpeakerResponseModel> GetSpeakerList()
        {

            Uri uri = new Uri(Constant.BaseUrl + "/Speakers/" + Util.CurrEvent_pkey + "/false");
            try
            {

                var speakerLists = await _requestService.PostAsyncAPi<SpeakerResponseModel>(uri);
                return speakerLists;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<SpeakerResponseModel> GetSortedSpeakerList()
        {

            Uri uri = new Uri(Constant.BaseUrl + "/Speakers/" + Util.CurrEvent_pkey + "/true");
            try
            {

                var speakerLists = await _requestService.PostAsyncAPi<SpeakerResponseModel>(uri);
                return speakerLists;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ProgramEventListModel> GetProgramEventDetails(string session_key)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/GETSessionDetails/" + session_key + "/true/" + Util.AccountPkey);
            try
            {
                var sessionDetails = await _requestService.PostAsyncAPi<ProgramEventListModel>(uri);
                return sessionDetails;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        
        public async Task<SpeakerDetailsResponseModel> GetSpeakerDetails(string speaker_id)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/AccountBIO/" + speaker_id + "/" + Util.CurrEvent_pkey);
            try
            {
                var speakerDetails = await _requestService.PostAsyncAPi<SpeakerDetailsResponseModel>(uri);
                return speakerDetails;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<CreateScheduleResponseModel> GetScheduleList(string account_key)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/CreateMySchedule/" + account_key + "/" + Util.CurrEvent_pkey);
            try
            {
                var schedulelistitems = await _requestService.PostAsyncAPi<CreateScheduleResponseModel>(uri);
                return schedulelistitems;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ViewMyScheduleResponseModel> GetScheduleActivityList(string account_key)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/VIEWMySchedule/" + account_key + "/" + Util.CurrEvent_pkey);
            try
            {
                var activityList = await _requestService.PostAsyncAPi<ViewMyScheduleResponseModel>(uri);
                return activityList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> CreateSchedule(string account_key, string pkey, string attend, string slides, string watch)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/SetAttend/" + account_key + "/" + pkey + "/" + attend + "/" + slides + "/" + watch);
            try
            {
                var updateScheduleList = await _requestService.PostAsyncAPi<string>(uri);
                return updateScheduleList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<TopicsResponseModel> GetTopicsList(string Event_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ListProfInterests/" + Event_pkey);
            try
            {
                var getTopicsList = await _requestService.PostAsyncAPi<TopicsResponseModel>(uri);
                return getTopicsList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<OrganizationResponseModel> GetOrganizationList(string pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/Participating_organizations/" + Util.CurrEvent_pkey + "/" + pkey);
            try
            {
                var organizationLists = await _requestService.PostAsyncAPi<OrganizationResponseModel>(uri);
                return organizationLists;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<UpcomingEventModel> GetUpcomingEventList()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/FutureConferences");
            try
            {
                var upcomingeventlist = await _requestService.PostAsyncAPi<UpcomingEventModel>(uri);
                return upcomingeventlist;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<EventTermAndConditionResponseModel> GetTermsAndConditionList(string number)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/Event_TermsandConditions/" + number);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<EventTermAndConditionResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ContactPageResponseModel> GetEventContactList()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/Contacts/" + Util.CurrEvent_pkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<ContactPageResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<FAQsResponseModel> GetFAQsList()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/FAQs/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<FAQsResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> PostReportAnIssue(UserIssueRequestModel userIssueRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/UserIssue");
            try
            {
                var json = JsonConvert.SerializeObject(userIssueRequestModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> PostImage(UserIssueRequestModel userIssueRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/UserIssue");
            try
            {
                var json = JsonConvert.SerializeObject(userIssueRequestModel);
                
                var T_CList = await _requestService.PostAsync<string>(uri, json);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<MAGIPageResponseModel> GetMAGIIcon()
        {
            var platform = DeviceInfo.Platform;
            string Apptype;
            if (platform == DevicePlatform.Android)
            {
                Apptype = "2";
            }
            else
            {
                Apptype = "1";
            }
            Uri uri = new Uri(Constant.BaseUrl + "/siteinfo/" + Util.CurrEvent_pkey + "/" + Apptype);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<MAGIPageResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<UpdatePageResponseModel> GetUpdateIcon()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/updates/" + Util.CurrEvent_pkey + "/" + "1");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<UpdatePageResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<OverViewResponseModel> GetOverviewIcon()
        {
            var platform = DeviceInfo.Platform;
            string Apptype;
            if (platform == DevicePlatform.Android)
            {
                Apptype = "2";
            }
            else
            {
                Apptype = "1";
            }
            Uri uri = new Uri(Constant.BaseUrl + "/eventinfo/" + Util.CurrEvent_pkey + "/" + Apptype);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<OverViewResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<NetworkingResponseModel> Newtworking(string type, string sector, string location)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/Networking/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey + "/" + type + "/" + sector + "/" + location);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<NetworkingResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<NetworkingOutgoingResponseModel> NewtworkingOutgoing()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/NetworkingOutgoing/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<NetworkingOutgoingResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<NetworkingIncomingResponseModel> NewtworkingComing()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/NetworkingInComing/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<NetworkingIncomingResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ForgotPasswordResponseModel> ForgotPasswordApi(string emailid)
        {
            ForgotRequestModel forgotRequestModel = new ForgotRequestModel();
            forgotRequestModel.Email = emailid;
            var fp = JsonConvert.SerializeObject(forgotRequestModel);
            Uri uri = new Uri(Constant.BaseUrl + "/ForgotPassword");
            try
            {
                var T_CList = await _requestService.PostAsync<ForgotRequestModel, ForgotPasswordResponseModel>(uri, forgotRequestModel);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<PhoneTypeResponseModel> PhoneTypesApi()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/PhoneTypes");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<PhoneTypeResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ReportIssueResponseModel> GetReportIssueList()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ListIssueCategories");
            try
            {
                var getIssueList = await _requestService.PostAsyncAPi<ReportIssueResponseModel>(uri);
                return getIssueList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<StateResponseModel> GetStateList(string countryp_Key)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/StateList/" + countryp_Key);
            try
            {
                var getStateList = await _requestService.PostAsyncAPi<StateResponseModel>(uri);
                return getStateList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<CountryResponseModel> GetCountryList()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/CountryList");
            try
            {
                var getCountryList = await _requestService.PostAsyncAPi<CountryResponseModel>(uri);
                return getCountryList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<TimezoneResponseModel> GetTimezoneList(string countryp_Key)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/TimeZoneList/" + countryp_Key);
            try
            {
                var getTimezoneList = await _requestService.PostAsyncAPi<TimezoneResponseModel>(uri);
                return getTimezoneList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<TimezoneResponseModel> GetTimezoneListforMeeting(string countryp_Key)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/TimeZoneListforMeeting/" + countryp_Key);
            try
            {
                var getTimezoneList = await _requestService.PostAsyncAPi<TimezoneResponseModel>(uri);
                return getTimezoneList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> NetworkingOutgoingSave(NetworkingOutgoingSaveModel networkingOutgoingSaveModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/NetworkingOutgoingSave");
            try
            {
                var json = JsonConvert.SerializeObject(networkingOutgoingSaveModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> NetworkingIncomingSave(NetworkingIncomingSaveModel networkingIncomingSaveModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/NetworkingInComingSave");
            try
            {
                var json = JsonConvert.SerializeObject(networkingIncomingSaveModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<SectorResponseModel> GetSectorList()
        {

            Uri uri = new Uri(Constant.BaseUrl + "/Sector");
            try
            {
                var SectorList = await _requestService.PostAsyncAPi<SectorResponseModel>(uri);
                return SectorList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ParticipationTypeResponseModel> ParticipationType()
        {

            Uri uri = new Uri(Constant.BaseUrl + "/ParticipationType/" + Util.CurrEvent_pkey);
            try
            {
                var ParticipationList = await _requestService.PostAsyncAPi<ParticipationTypeResponseModel>(uri);
                return ParticipationList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<MeetingPlannerResponseList> MeetingPlannerListApi(MeetingPlannerListItem meetingPlannerListItem)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/MeetingPlannerList");
            try
            {
                var json = JsonConvert.SerializeObject(meetingPlannerListItem);
                var T_CList = await _requestService.PostAsyncString<MeetingPlannerResponseList>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> MeetingPlannerSaveApi(MeetingPlannerSave meetingPlannerSave)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/MeetingPlannerSave");
            try
            {
                var json = JsonConvert.SerializeObject(meetingPlannerSave);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<MeetingLocationResponseModel> GetMeetingLocationModels(string Event_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/EventMeetinglocation/" + Event_pkey);
            try
            {
                var LocationList = await _requestService.PostAsyncAPi<MeetingLocationResponseModel>(uri);
                return LocationList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<EventAttendingResponseModel> GetEventAttendingApi(EventAttendingRequestModel eventAttendingRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/EventAttending");
            try
            {
                var json = JsonConvert.SerializeObject(eventAttendingRequestModel);
                var T_CList = await _requestService.PostAsyncString<EventAttendingResponseModel>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<MeetingPlannerInvitedResponseModel> GetMeetingPlannerInvitedApi(MeetingPlannerInvitedRequestModel meetingPlannerInvitedRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/MeetingPlannerInvited");
            try
            {
                var json = JsonConvert.SerializeObject(meetingPlannerInvitedRequestModel);
                var T_CList = await _requestService.PostAsyncString<MeetingPlannerInvitedResponseModel>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> GetMeetingDeleteApi(MeetingDeleteRequestModel meetingDeleteRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/MeetingDelete/" + meetingDeleteRequestModel.MeetingPlanner_pkey);
            try
            {
                var deleteList = await _requestService.PostAsyncAPi<string>(uri);
                return deleteList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<MeetingPlannerAttendedResponseModel> GetMeetingPlannerAttendedApi(string MeetingPlanner_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/MeetingPlannerAttended/" + MeetingPlanner_pkey);
            try
            {
                var PlannerAttendedList = await _requestService.PostAsyncAPi<MeetingPlannerAttendedResponseModel>(uri);
                return PlannerAttendedList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<MeetingPlannerEditResponseModel> GetMeetingPlannerEditApi(string MeetingPlanner_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/MeetingPlannerEdit/" + MeetingPlanner_pkey);
            try
            {
                var PlannerEditList = await _requestService.PostAsyncAPi<MeetingPlannerEditResponseModel>(uri);
                return PlannerEditList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> GetMeetingPlannerUpdateAttendedApi(MeetingPlannerUpdateRequestModel meetingPlannerSave)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/MeetingPlannerUpdateAttended");
            try
            {
                var json = JsonConvert.SerializeObject(meetingPlannerSave);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> GetDeleteAttendingApi(string pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/DeleteAttending/" + pkey);
            try
            {
                var DeleteAttendingList = await _requestService.PostAsyncAPi<string>(uri);
                return DeleteAttendingList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> GetMeetingPlannerAlInvitedApi(MeetingPlannerInvitedAll meetingPlannerAllInvited)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/MeetingPlannerInvited");
            try
            {
                var json = JsonConvert.SerializeObject(meetingPlannerAllInvited);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> GetMeetingStatusChangesApi(MeetingStatusChangesRequestModel meetingStatusChangesRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/MeetingStatusChanges");
            try
            {
                var json = JsonConvert.SerializeObject(meetingStatusChangesRequestModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> GetSendInvitaionApi(SendInvitaionRequestModel sendInvitaionRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/SendInvitaion");
            try
            {
                var json = JsonConvert.SerializeObject(sendInvitaionRequestModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<InvitedAnnouncementResponseModel> GetInvitedAnnouncementApi(string Announcement_pkey, string currentEvent_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/InvitedAnnouncement/" + Announcement_pkey + "/" + currentEvent_pkey);
            try
            {
                var InvitedAnnouncementList = await _requestService.PostAsyncAPi<InvitedAnnouncementResponseModel>(uri);
                return InvitedAnnouncementList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<QuestionListResponseModel> GetQuestionListApi(string currentEvent_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/QuestionList/" + currentEvent_pkey);
            try
            {
                var QuestionList = await _requestService.PostAsyncAPi<QuestionListResponseModel>(uri);
                return QuestionList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<PollingResponseModel> GetPollingListApi(PollingListRequestModel pollingListRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/QuestionList");
            try
            {
                var json = JsonConvert.SerializeObject(pollingListRequestModel);
                var T_CList = await _requestService.PostAsyncString<PollingResponseModel>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> AttendingQuestionSaveApi(AttendingQuestionSaveRequestModel attendingQuestionSaveRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/AttendingQuestionSave");
            try
            {
                var json = JsonConvert.SerializeObject(attendingQuestionSaveRequestModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<FeedbackResponseModel> GeEventSpeakerlistApi(string currentEvent_pkey, string EventSession_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/EventSpeakerlist/" + currentEvent_pkey + "/" + EventSession_pkey);
            try
            {
                var EventSpeakerlist = await _requestService.PostAsyncAPi<FeedbackResponseModel>(uri);
                return EventSpeakerlist;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<UpdateFeedbackResponseModel> GetEventSpeakerFeedback(string EventSession_pkey, string SpeakerAccountId)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/feedback_select/" + EventSession_pkey + "/" + SpeakerAccountId + "/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey);
            try
            {
                var EventSpeakerfeedback = await _requestService.PostAsyncAPi<UpdateFeedbackResponseModel>(uri);
                return EventSpeakerfeedback;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<QuestionResponseModel> GetQuestion(string EventSession_pkey, string Account_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/AttendeeQuestion_Select/" + EventSession_pkey + "/" + Account_pkey);
            try
            {
                var Question = await _requestService.PostAsyncAPi<QuestionResponseModel>(uri);
                return Question;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<AskQuestionsToSpeakerRequestModel> QuestionAskedApi(string Question_pkey, string Question, string EventSession_pkey, string Account_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/AttendeeQuestions_Save/" + Question_pkey + "/" + Question + "/" + EventSession_pkey + "/" + Account_pkey);
            //Uri uri = new Uri(Constant.BaseUrl + "/AttendeeQuestions_Save" + "/Question_pkey/" + "Question/" + "EventSession_pkey/" + "Account_pkey");
            try
            {
                //var json = JsonConvert.SerializeObject(askQuestionsToSpeakerRequestModel);
                var T_Clist = await _requestService.PostAsyncAPi<AskQuestionsToSpeakerRequestModel>(uri);
                return T_Clist;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> SpeakerFeedBackSaveApi(SpeakerFeedBackSaveRequestModel speakerFeedBackSaveRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/SpeakerFeedBackSave");
            try
            {
                var json = JsonConvert.SerializeObject(speakerFeedBackSaveRequestModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<EventFeedbackResponseModel> GeEventFeedbacklistApi(string currentEvent_pkey, string Account_PKey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/getFeedback/" + currentEvent_pkey + "/" + Account_PKey);
            try
            {
                var EventFeedbacklist = await _requestService.PostAsyncAPi<EventFeedbackResponseModel>(uri);
                return EventFeedbacklist;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> EventFeedbackRequestApi(EventFeedbackRequestModel eventFeedbackRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/postFeedBack");
            try
            {
                var json = JsonConvert.SerializeObject(eventFeedbackRequestModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<NotAttendeeSurveyResponseModel> GetSurveyRequestApi(string currentEvent_pkey, string Account_PKey, string Survey_key)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/getsurvey/" + Account_PKey + "/" + currentEvent_pkey + "/" + Survey_key);
            try
            {
                var Surveytxt = await _requestService.PostAsyncAPi<NotAttendeeSurveyResponseModel>(uri);
                return Surveytxt;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> PostSurveyRequestApi(NotAttendeeSurveyRequestModel surveyAnswers)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/postSurvey");
            try
            {
                var json = JsonConvert.SerializeObject(surveyAnswers);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<VersionResponseModel> GetVersionApi(GetVersionRequestedModel requestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/getVersion");
            try
            {
                var json = JsonConvert.SerializeObject(requestModel);
                var T_CList = await _requestService.PostAsyncString<VersionResponseModel>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<OrganizationJobResponse> Getorganizationwise_attendeeApi(string event_Pkey, string organization_Pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/organizationwise_attendee/" + event_Pkey + "/" + organization_Pkey);
            try
            {
                var Organizationtxt = await _requestService.PostAsyncAPi<OrganizationJobResponse>(uri);
                return Organizationtxt;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> GetlistProfInterestsApi()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ListProfInterests/" + "Event_pkey");
            try
            {
                var Organizationtxt = await _requestService.PostAsyncAPi<string>(uri);
                return Organizationtxt;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> GetEventMeetinglocationApi()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/EventMeetinglocation/" + "Event_pkey");
            try
            {
                var Organizationtxt = await _requestService.PostAsyncAPi<string>(uri);
                return Organizationtxt;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<AboutAppResponseModel> GetAboutAppApi()
        {
            var platform = DeviceInfo.Platform;
            string Apptype;
            if (platform == DevicePlatform.Android)
            {
                Apptype = "2";
            }
            else
            {
                Apptype = "1";
            }
            Uri uri = new Uri(Constant.BaseUrl + "/aboutapp/" + Util.CurrEvent_pkey + "/" + Apptype);
            try
            {
                var AboutApptxt = await _requestService.PostAsyncAPi<AboutAppResponseModel>(uri);
                return AboutApptxt;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<DDLResponseModel> GetSalutationListApi()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ddlList/" + "0/0");
            try
            {
                var SalutationList = await _requestService.PostAsyncAPi<DDLResponseModel>(uri);
                return SalutationList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<DDLResponseModel> GetSuffixListApi()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ddlList/" + "3/0");
            try
            {
                var SuffixList = await _requestService.PostAsyncAPi<DDLResponseModel>(uri);
                return SuffixList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<DDLResponseModel> GetLicenseListApi()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ddlList/" + "6/0");
            try
            {
                var LicenseList = await _requestService.PostAsyncAPi<DDLResponseModel>(uri);
                return LicenseList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<DDLResponseModel> GetAdviceListApi()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ddlSpeakerAdvice");
            try
            {
                var LicenseList = await _requestService.PostAsyncAPi<DDLResponseModel>(uri);
                return LicenseList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<SessionViewMyScheduleModel> GetSessionDetailsApi(string EventSession_pkey, string Type)
        {
            string FileName = "null";
            string IPaddress = "0.0.0.0";
            try
            {
                var Ipaddress = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault();
                IPaddress = Ipaddress.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            string Apptype;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Apptype = "2";
            }
            else
            {
                Apptype = "1";
            }
            Uri uri = new Uri(Constant.BaseUrl + "/GetPlayLinkandURL/" + EventSession_pkey + "/" + Util.AccountPkey + "/" + Type + "/" + Util.CurrEvent_pkey + "/" + FileName + "/" + IPaddress + "/" + Apptype);
            try
            {
                var SessionDetails = await _requestService.PostAsyncAPi<SessionViewMyScheduleModel>(uri);
                return SessionDetails;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ChatHistoryResponseModel> GetChatHistory(string id)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ChatHistoryByPerson/" + Util.AccountPkey + "/" + Util.CurrEvent_pkey + "/" + id);
            try
            {
                var ChatHistory = await _requestService.PostAsyncAPi<ChatHistoryResponseModel>(uri);
                return ChatHistory;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ChatPeopleResponseModel> GetChatPeople()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/GetChatHistory/" + Util.AccountPkey + "/" + Util.CurrEvent_pkey);
            try
            {
                var ChatHistory = await _requestService.PostAsyncAPi<ChatPeopleResponseModel>(uri);
                return ChatHistory;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ChatPeopleResponseModel> GetChatPeopleSearch(string search)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/GetChatPeople/" + Util.AccountPkey + "/" + Util.CurrEvent_pkey + "/" + search);
            try
            {
                var ChatHistory = await _requestService.PostAsyncAPi<ChatPeopleResponseModel>(uri);
                return ChatHistory;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ChatPeopleResponseModel> GetBio(string id)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/AccountBIO/" + id + "/" + Util.CurrEvent_pkey);
            try
            {
                var Bio = await _requestService.PostAsyncAPi<ChatPeopleResponseModel>(uri);
                return Bio;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }        
        public async Task<ChatConnectionResponseModel> GetConnectionStatus(string ConnectionAccount_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ConnectionRequest_status/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey + "/" + ConnectionAccount_pkey);
            //Uri uri = new Uri(Constant.BaseUrl + "/ConnectionRequest_status/" + Util.CurrEvent_pkey + "/" + ConnectionAccount_pkey + "/" + Util.AccountPkey);//new change
            try
            {
                var Bio = await _requestService.PostAsyncAPi<ChatConnectionResponseModel>(uri);
                return Bio;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }        
        public async Task<ChatConnectionResponseModel> GetConnectionStatusRequest(string ConnectionAccount_pkey, string status)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ConnectionStatus_Change/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey + "/" + ConnectionAccount_pkey + "/" + status);
            try
            {
                var Bio = await _requestService.PostAsyncAPi<ChatConnectionResponseModel>(uri);
                return Bio;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> GetLocationUpdate(GetVersionRequestedModel requestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/LocaltionUpdate");
            try
            {
                var json = JsonConvert.SerializeObject(requestModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> sendConnectionRequest(string Event_pkey, string ConnectionAccount_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ConnectionRequestWithQRCode/" + Event_pkey + "/" + ConnectionAccount_pkey + "/" + Util.AccountPkey);
            try
            {
                var connectionDetails = await _requestService.PostAsyncAPinew<string>(uri);
                return connectionDetails;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }   
        }

        public async Task<LocationPeopleSearchResponseModel> GetLocationSearch(string searchkeyword, string latitute, string longitude)
        {
           // Uri uri = new Uri(Constant.BaseUrl + "/Attendee_Search/" + Util.CurrEvent_pkey + "/" + searchkeyword + "/" + latitute + "/" + longitude);
            Uri uri = new Uri(Constant.BaseUrl + "/Attendee_Search/" + Util.CurrEvent_pkey + "/" + searchkeyword + "/" + "null" + "/" + "null");
            try
            {
                var Bio = await _requestService.PostAsyncAPi<LocationPeopleSearchResponseModel>(uri);
                return Bio;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ResourcesResponseModel> GetResources(string Eventorgpkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/Files_select/" + "RESOURCE/" + Eventorgpkey);
            try
            {
                var res = await _requestService.PostAsyncAPi<ResourcesResponseModel>(uri);
                return res;
            }
            catch (Exception ex) 
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> GetAttendeelogs(LogsRequestModel logsRequest)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/Attendee_Log");
            try
            {
                var json = JsonConvert.SerializeObject(logsRequest);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<PartnerEventResponseModel> GetEventsofpartners(string Eventorgpkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/EventSchedule" + "/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey + "/" + Eventorgpkey);
            try
            {
                var res = await _requestService.PostAsyncAPi<PartnerEventResponseModel>(uri);
                return res;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<EmailTypeResponseModel> EmailTypesApi()
        { 
            Uri uri = new Uri(Constant.BaseUrl + "/EmailTypes");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<EmailTypeResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<EventSummaryResponseModel> GetEventSummary()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/EventSummary/" + Util.AccountPkey + "/" + Util.CurrEvent_pkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<EventSummaryResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<byte[]> GetEventSummaryReceipt(string receiptnumber, string receipt, string paidBool)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ReceiptDownload/" + receiptnumber + "/" + receipt + "/" + paidBool);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<byte[]>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<IsVerifyUsedEmailResponseModel> IsVerifyUsedEmail(string email)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/IsVerifyUsedEmail/" + email + "/" + Util.AccountPkey);
            try
            {
                var UsedEmail  = await _requestService.PostAsyncAPi<IsVerifyUsedEmailResponseModel>(uri);
                return UsedEmail;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<NetworkingLevelResponseModel> GetNetworkingLevel()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/NetworkingLevel/" + Util.AccountPkey + "/" + Util.CurrEvent_pkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<NetworkingLevelResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ConnectResponseModel> GetConnects(string orgpkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/ConnectPeople/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey + "/" + orgpkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<ConnectResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<LunchOptionsResponseModel> GetLunchPreferences()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/LunchOption/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<LunchOptionsResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now) 
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<MealListResponseModel> GetMealList(string Meal_Pkey, string SpecialMealID)
        {
            try
            {
                Uri uri = null;
                //if (Convert.ToInt32(Meal_Pkey)>0)
                if (!string.IsNullOrEmpty(SpecialMealID))
                {
                    uri = new Uri(Constant.BaseUrl + "/MealList/" + Util.CurrEvent_pkey + "/" + Meal_Pkey + "/" + SpecialMealID);
                }
                else
                {
                    uri = new Uri(Constant.BaseUrl + "/MealList/" + Util.CurrEvent_pkey);
                }
                var T_CList = await _requestService.PostAsyncAPi<MealListResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<SaveMealRequestResponseModel> SaveMealRequest(string Meal_Pkey, string EventSession_pkey, string bAttends)
        {
            try
            {
                Uri uri = new Uri(Constant.BaseUrl + "/SaveMealRequest/" + Util.CurrEvent_pkey + "/" + Meal_Pkey + "/" + EventSession_pkey + "/" + bAttends + "/" + Util.AccountPkey);
                var T_CList = await _requestService.PostAsyncAPi<SaveMealRequestResponseModel>(uri);
                return T_CList;
            }   
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<SaveMealRequestResponseModel> ConfirmMealRequest(string Meal_Pkey, string EventSession_pkey)
        {
            try
            {
                Uri uri = new Uri(Constant.BaseUrl + "/ConfirmLunch/" + Util.CurrEvent_pkey + "/" + Meal_Pkey + "/" + EventSession_pkey + "/" + Util.AccountPkey);
                var T_CList = await _requestService.PostAsyncAPi<SaveMealRequestResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<SaveMealRequestResponseModel> ConfirmSpecialMealRequest(string Meal_Pkey, string EventSession_pkey, string SpecialMealRequestRequest)
        {
            try
            {
                Uri uri = new Uri(Constant.BaseUrl + "/SpecialMeal/" + Util.CurrEvent_pkey + "/" + Meal_Pkey + "/" + EventSession_pkey + "/" + Util.AccountPkey + "/" + SpecialMealRequestRequest);
                var T_CList = await _requestService.PostAsyncAPi<SaveMealRequestResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<BadgeInstructions> GetBadgeInstruction()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/Instruction/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<BadgeInstructions>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry<DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<EventBadgeResponseModel> GetBadgeInfo()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/BadgeInfo/" + Util.AccountPkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<EventBadgeResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<CEEAOptionsResponseModel> GetCEEAOptionsInfo()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/Examcharges/" + Util.CurrEvent_pkey + "/" + Util.AccountPkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<CEEAOptionsResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<EmailConnectionRequestModel> GetEmailConnection(string ConnectionAccount_pkey, string Msg, bool IncE, bool IncP, bool Request)
        {
            //Uri uri = new Uri(Constant.BaseUrl + $"/ConnectionrequestSave/{Util.CurrEvent_pkey}/{ConnectionAccount_pkey}/{Util.AccountPkey}/{Msg}/{IncE}/{IncP}/{Request}");
            Uri uri = new Uri(Constant.BaseUrl + $"/ConnectionrequestSave/{Util.CurrEvent_pkey}/{ConnectionAccount_pkey}/{Util.AccountPkey}/" + "\"" + Msg + "\"" + "/" + IncE  + "/" + IncP +"/" + Request);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<EmailConnectionRequestModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<BadgesSaveEditResponseModel> GetBadgeSave(string BName,string BTitle,string BOrganizationID)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/badgeSaveEdit/{Util.CurrEvent_pkey}/{Util.AccountPkey}/{BName}/{BTitle}/{BOrganizationID}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<BadgesSaveEditResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ResetBadge> GetBadgeResetInfo()
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/badgeReset/{Util.CurrEvent_pkey}/{Util.AccountPkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<ResetBadge>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ReceiptResponseModel> GetReceipt()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/GetReceipt");
            try
            {
                var response = await _requestService.PostAsyncAPi<ReceiptResponseModel>(uri);
                return response;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<PaymentFieldsResponseModel> GetPaymentFields()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/paymentGetWayInfo");
            try
            {
                var response = await _requestService.PostAsyncAPi<PaymentFieldsResponseModel>(uri);
                return response;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<SponsorscheduleResponseModel> GetSponsorSchedule(string EventOrganizationpKey)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/Show_Sponsor_Schedule/{Util.CurrEvent_pkey}/{Util.AccountPkey}/{EventOrganizationpKey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<SponsorscheduleResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<MeetingScheduleAvailabilityResponseModel> MeetingScheduleAvail(string SponsorAccount_pkey, string EventOrganizationpKey)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/Meeting_ScheduleAvailabl/{Util.CurrEvent_pkey}/{SponsorAccount_pkey}/{EventOrganizationpKey}/{Util.AccountPkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<MeetingScheduleAvailabilityResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<UpdateEventAttendeeScheduleResponseModel> GetMeetScheduleResult(string EventOrganizationpKey, string EventSponsorPerson_pKey, string BoothStaffSchedulepKey)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/Updateeventattendee_schedule/{Util.CurrEvent_pkey}/{Util.AccountPkey}/{EventOrganizationpKey}/{EventSponsorPerson_pKey}/{BoothStaffSchedulepKey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<UpdateEventAttendeeScheduleResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<PaymentUpdateResponseModel> GetPaymentUpdate(PaymentUpdateRequestModel PaymentRequest)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/PaymentUpdate");
            try
            {
                var json = JsonConvert.SerializeObject(PaymentRequest);
                var T_CList = await _requestService.PostAsyncString<PaymentUpdateResponseModel>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ChargesRefundResponseModel> GetChargesRefund()
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/ChargesRefund/{Util.AccountPkey}/{Util.CurrEvent_pkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<ChargesRefundResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<SaveExamchargesResponseModel> SaveExamcharges(SaveExamchargesRequestModel saveExamchargesRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/SaveExamcharges");
            try
            {
                var json = JsonConvert.SerializeObject(saveExamchargesRequestModel);
                var T_CList = await _requestService.PostAsyncString<SaveExamchargesResponseModel>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ChatStatusResponseModel> GetChatStatus()
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/ChatAllowed/{Util.CurrEvent_pkey}/{Util.AccountPkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<ChatStatusResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PollingVisibleResultResponseModel> GetVisibleResult(string EventSession_pkey, string Question_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/QuestionGraph/{EventSession_pkey}/{Question_pkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<PollingVisibleResultResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PollingManagementResponseModel> GetPollingManagement(string EventSession_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/SpeakerPollingResult/{EventSession_pkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<PollingManagementResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> GetTickIcon(string pkey,string IsStarted,string EventSession_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/POLLINGSTARTORNOT/{pkey}/{IsStarted}/{EventSession_pkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<string>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> GetEyeIcon(string pkey, string ShowResult, string EventSession_pkey)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/POLLINGRESULTSHOWORNOT/{pkey}/{ShowResult}/{EventSession_pkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<string>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<SpeakerDinnerResponseModel> GetSpeakerDinner()
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/SpeakerDinner/{Util.CurrEvent_pkey}/{Util.AccountPkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<SpeakerDinnerResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<LicenseTypeResponseModel> GetLicenseType()
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/LicenseType");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<LicenseTypeResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> UpdateLicenseinfo(UpdateLicenseRequestModel updateLicenseRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/UpdateLicenseinfo");
            try
            {
                var json = JsonConvert.SerializeObject(updateLicenseRequestModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<DinnerRefreshModel> GetDinnerRefresh()
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/SpeakerRefreshScreen/{Util.AccountPkey}/{Util.CurrEvent_pkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<DinnerRefreshModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<SpeakerDinnerSaveResponseModel> DinnerSave(SpeakerDinnerSave speakerDinnerSave)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/SpeakerDinnerSave");
            try
            {
                var json = JsonConvert.SerializeObject(speakerDinnerSave);
                var T_CList = await _requestService.PostAsyncString<SpeakerDinnerSaveResponseModel>(uri,json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> GetParticpantsNotesSave(ParticipentNote participentNote)
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/SaveParticipantsSessionNotes");
            try
            {
                var json = JsonConvert.SerializeObject(participentNote);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<TreasueHuntResponseModel> GetTreauseHunt()
        {
            Uri uri = new Uri(Constant.BaseUrl + $"/AttendeeTreasureHunt/{Util.CurrEvent_pkey}/{Util.AccountPkey}");
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<TreasueHuntResponseModel>(uri);
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<RegistrationSurveyDetailsModel> GetRegistrationSurveryStatus()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/SubmitQuestion/"  + Util.AccountPkey + "/" + Util.CurrEvent_pkey);
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<RegistrationSurveyDetailsModel>(uri);
                //var T_CList = "No";
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<RegistrationSurveyResponseModel> AttendeeRegistriongQuesSelect()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/AttendeeRegistriongQuesSelect/" + Util.CurrEvent_pkey );
            try
            {
                var RegistrationSurveylist = await _requestService.PostAsyncAPi<RegistrationSurveyResponseModel>(uri);
                return RegistrationSurveylist;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> RegistrationAttemptSaveApi()
        {
            Uri uri = new Uri(Constant.BaseUrl + "/RegiQuestion_Attempt/" +  Util.AccountPkey + "/" + Util.CurrEvent_pkey );
            try
            {
                var T_CList = await _requestService.PostAsyncAPi<string>(uri);
                //var T_CList = "No";
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> RegistrationSurveyQuestionSaveApi(RegistrationQuestionSaveRequestModel attendingQuestionSaveRequestModel)
        {
            Uri uri = new Uri(Constant.BaseUrl + "/RegistrationQuestionResponce_save");
            try
            {
                var json = JsonConvert.SerializeObject(attendingQuestionSaveRequestModel);
                var T_CList = await _requestService.PostAsyncString<string>(uri, json.ToString());
                return T_CList;
            }
            catch (Exception ex)
            {
                if (Util.Expiry < DateTime.Now)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}