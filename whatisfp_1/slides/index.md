- title : What is Functional Programming
- description : Introduction to Functional Programming in .NET
- author : Chris Jansson
- theme : night
- transition : default

***

### Functional programming
#### A brief introduction to functional concepts possibly in F#

<img src="http://fsharp.org/img/logo/fsharp256.png" alt="logo" style="width: 200px;"/>

***

***

### So what is functional programming?
- A programming paradigm
- Works by evluating expressions (think of them as mathemtical functions)
    - Is a combination of values and functions
    - Always has a return value


    let g(x) = x - 1
    let f(x) = x + 2 * g(x)

***

### An example
Statement

    public string Greet(string name)
    {
        string greeting = "";
        greeting += "Hello ";
        greeting += name;
        greeting += "!";
        return greeting;
    }


Expression

    public string Do(string name)
    {
        return $"Hello {name}!";
    }

' Statements does not need a return value
' Order is important

***

### So what are the benefits of FP?
- 

```csharp
    static int result;

    public void LoopStatement()
    {
        int i;    //what is the value of i before it is used? 
        int length;
        var array = new int[] { 1, 2, 3 };
        int a;  //what is the value of sum if the array is empty?

        length = array.Length;   //what if I forget to assign to length?
        for (i = 0; i < length; i++)
        {
            a += array[i];
        }

        result = a;
    }
```

' Order of execution is important
' Need to understand prior operations to reason about
' Does not compose no f(g(x)), needs glue code
' Does not trivially compose
' Expressions and subexpressions can be individually understood
' Declarative vs imperative
' Focus more on what to achieve compared to telling the computer the exact steps to do them

***

### The concept of purity
- Pure functional programming
- No side effects
- Expressions are pure

```csharp
    public int Calculate(int input) 
    {
        var a = 1 + input;
        var b = Monkey(input); //Result is unused, can it be removed?
        return a;
    }
```

' Input and output
' Easy to reason about
' Easy to test
' Explicit ordering
' Parallelizable
' Composable

***

### So what is an FP language?
- Some are 'purely functional' eg. Haskell
- Some are FP first eg. OCaml
- There isn't really such a thing as a language that does not do FP

' Maintaining an FP style in many languages requires a lot of effort though

***

### Properties of 'FP(-first)' languages
- Expression based
- First class functions
    * Currying and Partial Application
- Immutable (by default)
- Richer type System
- High degree of type inference

***

