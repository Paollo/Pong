using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SerializeObjectHelper
{
    #region Members
   
    
    #endregion
    
    #region Properties

    #endregion
    
    #region Methods

    public static void Serialize<T>(T objectSerialize)
    {       
        FileStream fs = new FileStream(Application.persistentDataPath+"\\BestScore.dat", FileMode.Create);
        
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, objectSerialize);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

    public static T Deserialize<T>() where T:new()
    {
        T deserializedObject = new T();

        FileStream fs = new FileStream(Application.persistentDataPath+"\\BestScore.dat", FileMode.OpenOrCreate);
        
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            deserializedObject = (T) formatter.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to deserialize. Reason: " + e.Message);
            
        }
        finally
        {
            fs.Close();
        }

        return deserializedObject;
    }    

    #endregion
    
    #region ClassesAndEnums

    #endregion 
}
