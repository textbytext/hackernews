﻿using HackerNews.WebAPI.Entities;

namespace HackerNews.WebAPI.Repositories;

using TNewsEntity = HackerNewsEntity;
using TNewsId = UInt64;

public class HackerNewsRepository : IHackerNewsProvider
{
    private Dictionary<TNewsId, TNewsEntity> _news = new Dictionary<TNewsId, TNewsEntity>();
    private TNewsEntity[] _newsByScore = Array.Empty<TNewsEntity>();

    public void AddRange(IEnumerable<TNewsEntity> news)
    {
        lock (_news!)
        {
            foreach (var n in news)
            {
                _news[n.ID] = n;
            }

            _newsByScore = _news.Values.OrderBy(i => i.Score).ToArray();
        }
    }

    public IEnumerable<TNewsEntity> NewsByScore() => _newsByScore;
}