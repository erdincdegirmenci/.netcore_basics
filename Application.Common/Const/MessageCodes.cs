namespace Application.Common.Const
{
    public class MessageCodes
    {
        //Hata kodları her bir business service grubu için ayrılmalı 10XXX, 11XXX gibi

        //Auth 10XXX
        public const string LOGIN_FAILED = "10001";
        public const string USER_NO_DATA_FOUND = "10101";//Bu maile ait kullanıcı tanımı bulunamamıştır.
        public const string USER_ALREADY_EXIST = "10102";//Girdiğiniz email ile kayıtlı kullanıcı sistemde bulunmaktadır.
        public const string USER_PASSIVE = "10103";//Girdiğiniz email ile kayıtlı kullanıcı pasif durumda.
        public const string MAIL_LINK_USED_OR_EXPIRE = "10104";//Girdiğiniz email kullanılmış ya da zaman aşımına uğramış.

        //Correspondance 20XXX
        public const string UPLOAD_File_FAILED = "20000";



        public const string RecordNotFound = "99999";//General Exception


     

    }
}
