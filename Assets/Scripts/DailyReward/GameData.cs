using UnityEngine;

public static class GameData {
	private static int _keys = 0;
	private static int _coins = 0;
	private static int _magnets = 0;
	private static int _stars = 0;
	private static int _rockets = 0;


	//Static Constructor to load data from playerPrefs
	static GameData ( ) {
		_keys = PlayerPrefs.GetInt ( "ownedKey", 0 );
		_coins = PlayerPrefs.GetInt ( "OwnedCoins", 0 );
		_magnets = PlayerPrefs.GetInt("ownMagnet", 0);
		_rockets = PlayerPrefs.GetInt("ownRocket", 0);
		_stars = PlayerPrefs.GetInt("ownStar", 0);
	}

	public static int Keys {
		get{ return _keys; }
		set{ PlayerPrefs.SetInt ( "ownedKey", (_keys = value) ); }
	}

	public static int Coins {
		get{ return _coins; }
		set{ PlayerPrefs.SetInt ( "OwnedCoins", (_coins = value) ); }
	}

	public static int Magnets
	{
		get { return _magnets; }
		set { PlayerPrefs.SetInt("ownMagnet", (_magnets = value)); }
	}

	public static int Stars
	{
		get { return _stars; }
		set { PlayerPrefs.SetInt("ownStar", (_stars = value)); }
	}

	public static int Rockets
	{
		get { return _rockets; }
		set { PlayerPrefs.SetInt("ownRocket", (_rockets = value)); }
	}

}
