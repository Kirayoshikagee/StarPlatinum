local TestLua = {}

TestLua.gameObject = nil
TestLua.transform = nil

function TestLua.Start()
    print("========================================")
    local newObj = XObject:Ctor("Common/newObj", nil, {testValue = "111"})
    print("newObj.ClassName:".. tostring(newObj.className))

    local newObj2 = XObject:Ctor("Common/newObj2")
    print("newObj2.ClassName:" .. tostring(newObj2.className))
    print("newObj2.testValue:" .. tostring(newObj2.testValue))
    print("newObj1.testValue:" .. tostring(newObj.testValue))

end

function TestLua.OnDestroy()
    TestLua.gameObject = nil
    TestLua.transform = nil
end

function TestLua:SayHello()
    print("TestLua:Hello xlua")
end

return TestLua