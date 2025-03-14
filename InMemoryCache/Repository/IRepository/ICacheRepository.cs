﻿namespace InMemoryCache.Repository.IRepository
{
    public interface ICacheRepository
    {
        T GetData<T>(string key);
        object RemoveData(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
    }
}
