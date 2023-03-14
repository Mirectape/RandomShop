using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveClient (ShopManager shopManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/client.shopAppPlus";
        FileStream stream = new FileStream(path, FileMode.Create);

        ClientData clientData = new ClientData(shopManager);

        formatter.Serialize(stream, clientData);
        stream.Close();
    }

    public static ClientData LoadClient()
    {
        string path = Application.persistentDataPath + "/client.shopAppPlus";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            ClientData clientData = formatter.Deserialize(fileStream) as ClientData;
            fileStream.Close();

            return clientData;
        }
        else
        {
            Debug.Log("Saved file not found");
            return null;
        }
    }
}
