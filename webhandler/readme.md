# c# implementation using raw webhandler

Pros:
- conceptually easy to understand
- easy to mock the httpcontext (since you only use a subset)
- raw: no help with mapping posted data to types
Cons:
- raw: no help with mapping posted data to types
- standard url is not pretty (can be fixed by web config redirect or global asax redirect)
