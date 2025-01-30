var message = "Allahu Akbar";
console.log(message);
//Tuple in TS
// Declare 
var x;
//Initialize it
x = ["hello", 100];
console.log(x[0], x[1]);
// Any Data Type in TS (Basically Any DataType Evaluated Runtime)
var something = 5;
var something2 = "hahaha";
console.log(something);
////// <--------------------------- OOP Concept in ts ------------------------------------>
var Greeter = /** @class */ (function () {
    function Greeter(message) {
        this.greeting = message;
    }
    Greeter.prototype.greet = function () {
        return "Hello, " + this.greeting;
    };
    return Greeter;
}());
var greeter = new Greeter("world");
console.log(greeter.greet());
////// Interface
function printLabel(labeledObj) {
    console.log(labeledObj.label);
}
var myObj = { size: 10, label: "Size 10 Object" };
printLabel(myObj);
var Employee = /** @class */ (function () {
    function Employee(code, name) {
        this.empCode = code;
        this.name = name;
    }
    Employee.prototype.getSalary = function (empCode) {
        return "20000";
    };
    return Employee;
}());
var emp = new Employee(1, "Steve");
console.log(emp);
////// Parameter properties
var Octopus = /** @class */ (function () {
    function Octopus(name) {
        this.name = name;
        this.numberOfLegs = 8;
    } //// here name property declare inline in constructor parameters . 
    return Octopus;
}());
var oct = new Octopus("Puppy");
console.log(oct.name);
//# sourceMappingURL=demo.js.map