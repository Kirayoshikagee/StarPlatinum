
XObject = XObject or {}

XObject.className = "XObject"

function XObject:Ctor(path, superObj, o)
    local o = o or {}
    o.super = superObj or XObject
    o.classpath = path
    o.className = string.sub(path, string.find(path, "/")+1, string.len(path))

    setmetatable(o, self)

    

    return o
end