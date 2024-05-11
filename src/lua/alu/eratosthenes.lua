Eratosthenes = class(CrystalBenchmarkBase, function(eratosthenes, writer)
    CrystalBenchmarkBase.init(eratosthenes, writer, "Eratosthenes")
end)

Eratosthenes.N = 1024

function Eratosthenes:benchImplementation()
    local p, k, count, cnt = 0, 0, 0, 0
    local flag = {}

    while not self:isCancelled() do
        cnt = 1

        for i = 1, Eratosthenes.N, 1 do flag[i] = true end

        for i = 1, Eratosthenes.N, 1 do
            if flag[i] then
                p = i + i + 3
                k = i + p
                while k <= Eratosthenes.N do
                    flag[k] = false
                    k = k + p
                end
                cnt = cnt + 1
            end

        end

        count = count + 1
    end

    return count / 1000.0 / 100.0 * Eratosthenes.N
end
