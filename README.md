# SharpCache

Small in-memory cache implementation written in C#.

I built this to better understand how basic caching mechanisms work internally 
instead of relying on libraries like MemoryCache or Redis.

## Why

Most of the time we just use existing solutions.
I wanted to see how hard it actually is to build:

- TTL support
- Basic eviction
- Thread safety
- Size limiting

Turns out it’s simple in concept, but edge cases appear quickly.

## Features

- Generic key/value
- Optional expiration
- Basic LRU-style cleanup
- Lock-based thread safety
- No external dependencies

## Notes

This is not intended for production use.
There are many improvements possible:
- Background cleanup task
- True LRU implementation
- ReaderWriterLockSlim instead of lock
- Metrics support

Mostly an educational project.

## Build
