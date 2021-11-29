namespace Konfigurator.ViewModels
{
    public class LogInViewModel
    {
        private int _id;

        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                
            }
        }

        
        private int _password;

        public int password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                
            }
        }
    }
}