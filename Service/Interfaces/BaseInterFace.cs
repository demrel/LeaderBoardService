using System;
namespace  LeaderBoardService.Service.Interfaces
{
    public interface BaseInterFace<T>
    {
            T GetById(int id);
            void Add(T item);
            void Edit(T item);
            void Delete(int id);
    }
}
