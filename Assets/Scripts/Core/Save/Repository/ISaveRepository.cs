using Core.Save.Models;

namespace Core.Save.Repository
{
    public interface ISaveRepository
    {
        void Save(SaveData saveData);
        SaveData Load();
        bool HasSave();
    }
} 