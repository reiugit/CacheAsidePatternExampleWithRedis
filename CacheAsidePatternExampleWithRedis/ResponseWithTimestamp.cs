namespace CacheAsidePatternExampleWithRedis;

public record ResponseWithTimestamp(string Response, DateTimeOffset CachedAt);
