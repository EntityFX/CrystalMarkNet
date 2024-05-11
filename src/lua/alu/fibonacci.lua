Fibonacci = class(CrystalBenchmarkBase, function(fibonacci, writer)
    CrystalBenchmarkBase.init(fibonacci, writer, "Fibonacci")
end)

function Fibonacci:benchImplementation()
    local count = 0
    local n = 0

    local a, a1, b, b1, c, c1, x, x1, y, y1 = 0, 0, 0, 0, 0, 0, 0, 0, 0, 0

    while not self:isCancelled() do
        n = count + 2
        a = 1
        b = 1
        c = 0
        x = 1
        y = 0
        n = n - 1
        while n > 0 do
            if (bitand(n, 1) == 1) then
                x1 = x
                y1 = y
                x = a * x1 + b * y1
                y = b * x1 + c * y1
            end
            n = math.floor(n / 2)
            a1 = a
            b1 = b
            c1 = c
            a = a1 * a1 + b1 * b1
            b = b1 * (a1 + c1)
            c = b1 * b1 + c1 * c1
        end

        count = count + 1
    end

    return count / 910.974
end
