Napierian = class(CrystalBenchmarkBase, function(fibonacci, writer)
    CrystalBenchmarkBase.init(fibonacci, writer, "Napierian")
end)

Napierian.N = 225

Napierian.RADIXBITS = 15;

Napierian.RADIX = 2 ^ Napierian.RADIXBITS;

function Napierian:benchImplementation()
    local count = 0
    local a = {}
    local t = {}

    while not self:isCancelled() do
        local m, k = 0, 0

        for ix = 1, Napierian.N, 1 do
            a[ix] = 0
            t[ix] = 0
        end

        a[1] = 2
        t[2] = math.floor(Napierian.RADIX / 2)
        a[2] = t[2]
        k = 3
        m = 1

        while true do
            m = Napierian:divs(m, t, k, t)
            if m > Napierian.N then
                break
            end
            Napierian:add(a, t, a)
            k = k + 1
        end

        count = count + 1
    end

    return count / 0.3197 * 4
end

function Napierian:divs(m, a, x, b)
    local i, t = 0, 0

    for i = m, Napierian.N, 1 do
        t = lshift(t, Napierian.RADIXBITS) + a[i]
        b[i] = math.floor(t / x)
        t = t % x
    end

    if 2 * t >= x then
        i = Napierian.N
        while true do
            b[i] = b[i] + 1
            if bitand(b[i], Napierian.RADIX) ~= 1 then
                break
            end
            b[i] = bitand(b[i], Napierian.RADIX - 1)
            i = i - 1
        end
    end

    if b[m] ~= 0 then
        return m
    else
        return m + 1
    end
end

function Napierian:add(a, b, c)
    local i, u = 0, 0
    for i = Napierian.N, 1, -1 do
        u = u + a[i] + b[i]
        c[i] = bitand(u, Napierian.RADIX - 1)
        u = rshift(u, Napierian.RADIXBITS)
    end
end
