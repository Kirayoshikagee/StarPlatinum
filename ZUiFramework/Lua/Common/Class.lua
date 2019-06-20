--以函数闭包的形式实现面向对象, 可以参考的例子
  
--定义一个方法，函数闭包实现一个类的概念  
function People(name)  
    local self = {}  
  --初始化方法，私有的  
    local function init()  
        self.name = name  
    end  
    
    self.sayHi = function ()  
        print("Hello "..self.name)  
    end  
  
  --调用初始化  
    init()  
    return self  
end  
  
--实例化一个对象  
local p = People("ZhangSan")
p:sayHi()  
  
--函数闭包的形式实现类继承  
function Man(name)  
    local self = People(name)  
      
--  local function init()  
--        
--  end  
  
    self.sayHello = function ()  
        print("Hi "..self.name)  
    end  
      
    return self  
end  
  
local m = Man("Lisi")  
--m:sayHello()  
m:sayHi()