using System;
using UnityEngine;

namespace GameFramework.Graph
{
    public struct NodeId
    {
        public SerializableGuid Id ;
    }

    //Serializable GUID struct
    [Serializable]
    public struct SerializableGuid
    {
        [SerializeField] private byte[] guidBytes;

        public SerializableGuid(Guid guid)
        {
            guidBytes = guid.ToByteArray();
            
        }
        

        
        public static SerializableGuid NewGuid()
        {
            return new SerializableGuid(Guid.NewGuid());
        }

        public Guid ToGuid()
        {
            return new Guid(guidBytes);
        }

        public static implicit operator Guid(SerializableGuid serializableGuid)
        {
            return serializableGuid.ToGuid();
        }

        public static implicit operator SerializableGuid(Guid guid)
        {
            return new SerializableGuid(guid);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int p = 16777619;
                int hash = (int)2166136261;

                for (int i = 0; i < guidBytes.Length; i++)
                    hash = (hash ^ guidBytes[i]) * p;

                return hash;
            }
        }

        public override string ToString()
        {
            return ToGuid().ToString();
        }
    }
}