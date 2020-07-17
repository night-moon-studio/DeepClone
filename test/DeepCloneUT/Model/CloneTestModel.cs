﻿using DeepClone;
using System;
using System.Collections.Generic;

namespace NatashaUT.Model
{
    public class FieldCloneNormalModel
    {
        public const int Const=100;
        public readonly int ReadOnly;
        public CloneEnum Flag;
        public int Age;
        public string Name;
        public bool Title;
        public DateTime Timer;
        public decimal money;
        public long Id;
    }

    public enum CloneEnum
    {
        A,B,C
    }

    public class FieldCloneArrayModel
    {
       
        public string[] Name;
    }

    public class FieldCloneClassArrayModel
    {

        public FieldCloneNormalModel[] Models;
    }

    public class FieldCloneSubNodeModel
    {
       
        public FieldCloneNormalModel Node;
    }

    public class FieldCloneClassCollectionModel
    {

        public List<FieldCloneNormalModel> Nodes;
    }


    public class PropCloneNormalModel
    {
        public readonly bool NoUseCtor;
        public PropCloneNormalModel()
        {
            NoUseCtor = true;
            ReadOnly = 123;
            ReadOnlyString = "2323";
            ReadOnlyString1 = "3232";
        }



        public PropCloneNormalModel(string Readonly, string abc, string readonlyString1, string asds)
        {

            ReadOnlyString = abc;
            ReadOnlyString1 = readonlyString1;

        }

        public PropCloneNormalModel(string Readonly, string abc, string asds)
        {

            ReadOnlyString = abc;

        }
        public PropCloneNormalModel(int Readonly)
        {

            ReadOnly = Readonly;

        }
        public const int Const = 100;
        [NeedCtor]
        public readonly int ReadOnly;
        [NeedCtor("abc")]
        public readonly string ReadOnlyString;
        [NeedCtor]
        public readonly string ReadOnlyString1;
        public int Age;
        public string Name;
        public bool Title;
        public DateTime Timer;
        public decimal money;
        public long Id;
    }

    public class PropCloneArrayModel
    {

        public string[] Name { get; set; }
    }

    public class PropCloneClassArrayModel
    {

        public PropCloneNormalModel[] Models { get; set; }
    }

    public class PropCloneSubNodeModel
    {

        public PropCloneNormalModel Node { get; set; }
    }
    public class PropCloneClassCollectionModel
    {
        public List<PropCloneNormalModel> Nodes { get; set; }
    }
    public class CloneCollectionModel
    {
        public List<PropCloneNormalModel>[] ALNodes { get; set; }
        public List<PropCloneNormalModel[]> LANodes { get; set; }
        public IEnumerable<List<PropCloneNormalModel>> LLNodes { get; set; }
    }

    public class CloneDictModel
    {
        public Dictionary<string,string> Dicts;
    }
    public class CloneDictCollectionModel
    {
        public Dictionary<List<string>, List<FieldCloneNormalModel>> Dicts;
    }
    public class CloneDictArrayModel
    {
        public IDictionary<string, FieldCloneNormalModel[]>[] Dicts;
    }

    public class FieldLinkModel
    {
        public LinkedList<FieldLinkModel> Nodes;
        public string Name;
        public int Age;
        public FieldLinkModel()
        {
            Nodes = new LinkedList<FieldLinkModel>();
        }
    }

    public class FieldLinkArrayModel
    {
        public LinkedList<FieldLinkArrayModel>[] Nodes;
        public string Name;
        public int Age;
    }

    public class FieldSelfLinkModel
    {
        public FieldSelfLinkModel Next;
        public string Name;
        public int Age;
    }

    public class FieldSelfLinkArrayModel
    {
        public FieldSelfLinkArrayModel[] Next;
        public string Name;
        public int Age;
    }
}
