using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class VectorSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context){
        if(obj == null) return;
        
        Vector2 vector = (Vector2)obj;
        if(vector == null) return;

        info.AddValue("x", vector.x);
        info.AddValue("y", vector.y);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector){
        if(obj == null) return obj;

        Vector2 vector = (Vector2)obj;

        vector.x = (float)info.GetValue("x", typeof(float));
        vector.y = (float)info.GetValue("y", typeof(float));

        obj = vector;
        return obj;
    }
}
