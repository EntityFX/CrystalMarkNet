FFT = class(CrystalBenchmarkBase, function(fft, writer)
    CrystalBenchmarkBase.init(fft, writer, "FFT")
end)

FFT.N = 128

function FFT:benchImplementation()
    local count = 0
    local PI = math.pi

    local k, i, j = 0, 0, 0
    local w, wr, wi, x, xr, xi = 0.0, 0.0, 0.0, 0.0, 0.0, 0.0

    local fr = {}
    local fi = {}

    for ix = 1, FFT.N, 1 do
        fr[ix] = 0
        fi[ix] = 0
    end

    while not self:isCancelled() do
        for ix = 1, FFT.N, 1 do
            x = -2 * PI + ix * 4.0 * PI / FFT.N
            fr[ix] = math.sin(x) + math.sin(2.0 * x)
            fi[ix] = 0.0
        end

        fr = self:bitreverse(fr, FFT.N)
        fi = self:bitreverse(fi, FFT.N)

        k = 1
        while k < FFT.N do
            j = 1
            while j <= k do
                w = 2.0 * PI / (2.0 * k)
                wr = math.cos(w * j)
                wi = -math.sin(w * j)

                i = j
                while i <= FFT.N do
                    xr = wr * fr[i + k] - wi * fi[i + k]
                    xi = wr * fi[i + k] + wi * fr[i + k]
                    fr[i + k] = fr[i] - xr
                    fi[i + k] = fi[i] - xi
                    fr[i] = fr[i] + xr
                    fi[i] = fi[i] + xi

                    i = i + (k * 2)
                end
                j = j + 1
            end
            k = k * 2
        end

        count = count + 1
    end

    return (count * FFT.N) / 1153.3980
end

function FFT:bitreverse(a, N)
    local j, k, l = 0, 0, 0
    local b = {}
    for i = 1, N do
        b[i] = 0
    end

    for i = 1, N, 1 do
        k = 1
        l = math.floor(N / 2)

        j = 1
        while j < N do
            if bitand(i, j) == 1 then
                k = k + l
            end
            l = math.floor(l / 2)
            j = j * 2
        end

        b[i] = a[k];
    end

    for i = 1, N, 1 do
        a[i] = b[i]
    end

    return a
end
