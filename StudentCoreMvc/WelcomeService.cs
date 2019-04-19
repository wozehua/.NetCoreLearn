namespace StudentCoreMvc
{
    public class WelcomeService:IWelcomeServices
    {
        public string GetMessage()
        {
            return "Hello Welcome IWelcome Service";
        }
    }
}