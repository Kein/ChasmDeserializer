using ChasmDeserializer.Model.SaveGameData.WorldState.Saveable;

namespace ChasmDeserializer.Model.SaveGameData.WorldState
{
    public class PropData
    {
        public string PropFullName;
        public GenericProp PopData;

        public PropData(string propFullName, GenericProp popData)
        {
            PropFullName = propFullName;
            PopData = popData;
        }
    }
}
