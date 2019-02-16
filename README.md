![Build status](https://fyzxs.visualstudio.com/GitHub/_apis/build/status/InterfaceMocks_Universal) |

# Welcome to IMock!

IMock is a framework to create mock objects in environments that don't support traditional Mocking. It can be used in mock-able environments as well.

Examples Platform or languages: 
* C# on Universal Windows Platform
* Swift (Currently under construction)

The C# version is actively used for UWP projects where normal mocking frameworks can not be used. It's also used on other projects due to the simplicity over mocking frameworks.

#### What does this do?
The intended purpose of the library is three things to simplify creation of Mocks.

1) Able to provide multiple values
2) Able to throw exceptions
3) Able to interrogate method parameters

That's it.

This library takes care of 90% of what most Mock frameworks are used for. The development style I follow, [microObjects](https://quinngil.com/uobjects), has us interfacing everything.
The interfacing of everything removes the need for the other 10% I've seeen Mocking frameworks required for.

An usage example for `ISomeInterface`.
```
public interface ISomeInterface{
    string MethodName();
}
public sealed partial class MockSomeInterface : ISomeInterface{

    private MockMethodWithResponse&lt;string> _methodName;

    private MockTobeMocked(){}

    public string MethodName() => _methodName.Invoked();

    public sealed class Builder{
        private MockMethodWithResponse&lt;string> _methodName;= new MockMethodWithResponse&lt;string>("MockSomeInterface#MethodName");

        public Builder MethodName(params string[] responseValues){
            _methodName.UpdateInvocation(responseValues);
            return this;
        }
///
        public MockSomeInterface Build{
            return new MockSomeInterface{
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
    
    MockSomeInterface MockSomeInterface = new MockSomeInterface.Builder().Method("ValueToReturn").Build();
    
    ClassUnderTest subject = new ClassUnderTest(MockSomeInterface);

    //Act
    string actual = subject.ReturnsResultFromMethod();

    //Assert
    actual.Should().Be("ValueToReturn");
}
```

This has worked very well and very rapidly in our development experience. Which is why I'm trying to make it available as a resource for others.


## Utils
#### ReSharper plugin
To generate the `MockSomeInterface` classes easily for C# a ReSharper plugin has been built. The source code is available under the utils directory.
This plugin adds `Create Mock` and `Update Mock` to the `Alt+Enter` Context Action menu. We've found it saves a lot of time; though doing it some time manually will help provide a better understanding. Things being black magic is unfortunate.

Currently the plugin is not available on the ReSharper extensions marketplace. The nupkg files are included with the project.
