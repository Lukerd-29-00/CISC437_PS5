namespace DOOR.Shared.Utils
{
    public interface IOraTransMsgs
    {
        public void LoadMsgs();
        public string TranslateMsg(string strMessage);
    }
}
