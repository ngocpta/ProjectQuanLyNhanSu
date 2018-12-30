namespace WebAPI.Model.Constant
{
  public static class MemberStatus
  {
        public const string MemberNotFound = "MEMBER_NOT_FOUND"; // khong tim thay member
        public const string MemberDisabled = "MEMBER_DISABLED"; // member da bi disable hoac bi xoa
        public const string PasswordNotMatch = "PASSWORD_NOT_MATCH"; // password cu khong khop trong db
        public const string MemberUnauthorize = "UNAUTHORIZED";//member khong duoc quyen truy cap 
        public const string NotLogin = "NOT_LOGIN";//member khong login
        public const string HaveNotToken = "HAVE_NOT_TOKEN";//khong co token
        public const string IncorrectToken = "INCORRECT_TOKEN";// token sai
        public const string MemberAlreadyExists = "MEMBER_ALREADY_EXISTS";// Username da ton tai
      public const string PasswordWrongFormat = "PASSWORD_WRONG_FORMAT";//password moi nhap khong dung dinh dang
  }
}