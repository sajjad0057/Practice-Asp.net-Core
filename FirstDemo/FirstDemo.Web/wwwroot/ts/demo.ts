let message : string = "Allahu Akbar";
console.log(message);


//Tuple in TS
// Declare 
let x: [a:string, b:number];

//Initialize it

x = ["hello", 100];

console.log(x[0], x[1]);

// Any Data Type in TS (Basically Any DataType Evaluated Runtime)

let something : Object = 5;
let something2 : any = "hahaha"

console.log(something);


////// <--------------------------- OOP Concept in ts ------------------------------------>

class Greeter {
    greeting: string;
    constructor(message: string) {
        this.greeting = message;
    }
    greet() {
        return "Hello, " + this.greeting;
    }
}

let greeter = new Greeter("world");

console.log(greeter.greet());


////// Interface


function printLabel(labeledObj: { label: string }) {
    console.log(labeledObj.label);
}

let myObj = { size: 10, label: "Size 10 Object" };
printLabel(myObj);



interface IEmployee {
    empCode: number;
    name: string;
    getSalary: (n) => string;    //// here string is returned datatype .
}
class Employee implements IEmployee {
    empCode: number;
    name: string;
    constructor(code: number, name: string) {
        this.empCode = code;
        this.name = name;
    }
    getSalary(empCode: number): string {
        return "20000";
    }
}
let emp = new Employee(1, "Steve");
console.log(emp)


////// Parameter properties


class Octopus {
    readonly numberOfLegs: number = 8;
    constructor(readonly name: string) { }   //// here name property declare inline in constructor parameters . 
}


let oct: Octopus = new Octopus("Puppy");

console.log(oct.name)