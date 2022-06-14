[System.Serializable]
public class GameData
{
    private static GameData _instance;

    public static GameData Instance
    {
        get
        {
            if (_instance == null) _instance = new GameData();
            return _instance;
        }
    }
        
    public int score = 0;
    public int currentLevel;
    public float currentHealth;
    public float maxHealth = 100;
}