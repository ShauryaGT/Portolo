using System;
namespace MAGIApp.Model
{
    public class UserModel
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Account_pkey { get; set; }
        public  string CurrEvent_pkey { get;  set; }
        public string EventName { get; set; }
        public string eventtypeid { get; set; }
        public string Image { get; set; }
        public DateTime Expiry { get; set; }
        public bool isChecked { get; set; }
        public bool isLogin { get; set; }
        public  string Country;
        public  string OtherState;
        public  string State;
        public  string City;
        public  string State_pKey;
        public string Useruser_Name { get; set; }
        public string User_Email { get; set; }
        public string eventuserstatusid { get; set; }
        public DateTime startdayofevent { get; set; }
        public DateTime enddayofevent { get; set; }
        public string Region { get; set; }
        public string RegionCode { get; set; }
        public string TimeOffset { get; set; }
        public string ISEventFeedbackResponse { get; set; }
        public string Bannerimageurl { get; set; }
        public string LocationTimeInterval { get; set; }
        public string Nickname { get; set; }
        public string Organization { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RegistrationLevel_Pkey { get; set; }
        public string IsLicenseNumber { get; set; }
        public string IsSpeaker { get; set; }
    }
}
