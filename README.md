**IFake Framework**
* Build     : ![Build status](https://fyzxs.visualstudio.com/IFake/_apis/build/status/InterfaceFakes_Universal)
* ![Nuget version](https://fyzxs.feeds.visualstudio.com/_apis/public/Packaging/Feeds/8b20e113-93ce-44d9-83e5-7e3dd6153ea1/Packages/700d0907-bb91-4135-b82e-0d08d5247b22/Badge)

**Release Deployments**
* Nuget Feed : ![Nuget Feed](https://fyzxs.vsrm.visualstudio.com/_apis/public/Release/badge/de1edd3b-7499-47c7-937f-39d941f079bd/1/6)
* Nuget Pkg  : ![Nuget Package](https://fyzxs.vsrm.visualstudio.com/_apis/public/Release/badge/de1edd3b-7499-47c7-937f-39d941f079bd/1/1)
* Binary Zip : ![Binary Zip](https://fyzxs.vsrm.visualstudio.com/_apis/public/Release/badge/de1edd3b-7499-47c7-937f-39d941f079bd/1/2)
* R# Plugin  : ![Resharper Plugin](https://fyzxs.vsrm.visualstudio.com/_apis/public/Release/badge/de1edd3b-7499-47c7-937f-39d941f079bd/1/3)


# Welcome to IFake!

IFake is a framework to create fake objects in environments that don't support traditional fakeing. It can be used in fakeable environments as well.

Examples Platform or languages: 
* C# on Universal Windows Platform
* Swift (Currently under construction)

The C# version is actively used for UWP projects where normal mocking frameworks can not be used. It's also used on other projects due to the simplicity over mocking frameworks.

#### What does this do?
The intended purpose of the library is three things to simplify creation of Fakes.

1) Able to provide multiple values
2) Able to throw exceptions
3) Able to interrogate method parameters

That's it.

This library takes care of 90% of what most fake frameworks are used for. The development style I follow, [microObjects](https://quinngil.com/uobjects), has us interfacing everything.
The interfacing of everything removes the need for the other 10% I've seeen fakeing frameworks required for.

An usage example for `ISomeInterface`.
```
public interface ISomeInterface{
    string MethodName();
}
public sealed partial class FakeSomeInterface : ISomeInterface{

    private FakeMethodWithResponse&lt;string> _methodName;

    private FakeTobeFakeed(){}

    public string MethodName() => _methodName.Invoked();

    public sealed class Builder{
        private FakeMethodWithResponse&lt;string> _methodName;= new FakeMethodWithResponse&lt;string>("FakeSomeInterface#MethodName");

        public Builder MethodName(params string[] responseValues){
            _methodName.UpdateInvocation(responseValues);
            return this;
        }
///
        public FakeSomeInterface Build{
            return new FakeSomeInterface{
                _methodName = _methodName
            }
        }
    }
}
```

In the test we now use it as
```
[TestMethod]
public void ShouldFooGivenBar(){
    //Arrange
    
    FakeSomeInterface fakeSomeInterface = new FakeSomeInterface.Builder().Method("ValueToReturn").Build();
    
    ClassUnderTest subject = new ClassUnderTest(fakeSomeInterface);

    //Act
    string actual = subject.ReturnsResultFromMethod();

    //Assert
    actual.Should().Be("ValueToReturn");
}
```

This has worked very well and very rapidly in our development experience. Which is why I'm trying to make it available as a resource for others.


## Utils
#### ReSharper plugin
To generate the `FakeSomeInterface` classes easily for C# a ReSharper plugin has been built. The source code is available under the utils directory.
This plugin adds `Create Fake` and `Update Fake` to the `Alt+Enter` Context Action menu. We've found it saves a lot of time; though doing it some time manually will help provide a better understanding. Things being black magic is unfortunate.

Currently the plugin is not available on the ReSharper extensions marketplace. The nupkg files are included with the project.