# GritsenkoD

## Structure

### Each module should be exist in the `./src/*` folder.
~~~
./src
    |> M01
        |> M01.sln
        |> {Project}
            |> {Project}.csproj
    |> M02
        |> M02.sln
        |> {Project}
            |> {Project}.csproj
        |> {Project}.Tests
            |> {Project}.Tests.csproj
~~~

### Tests projects should exist near the target project, with name: `<TargetProject>.Tests`

### Use latest framework, currently it is the `net5.0`
