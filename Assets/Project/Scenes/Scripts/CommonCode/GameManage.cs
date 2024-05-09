public class GameManage
{
    private static GameManage instance;

    public static GameManage Instance()
    {
        if (instance is null)
        {
            instance = new GameManage();
        }
       
        return instance;
    }
}
