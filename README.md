# maana-xr-unity-graphql-test
Simple test using the [Maana Unity GraphQL client](https://github.com/maana-io/maana-xr-unity-graphql-client) in a Unity project.

## Git submodules
The client is added to the test project as an asset using git submodules:
```
git submodule add git@github.com:maana-io/maana-xr-unity-graphql-client.git unity-test-project/Assets/Maana/GraphQL
```

When you clone a repo containing submodules, git will not automatically clone them.  You must:
```
git submodule init
git submodule update
```
