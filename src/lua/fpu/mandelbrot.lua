Mandelbrot = class(CrystalBenchmarkBase, function(mandelbrot, writer)
    CrystalBenchmarkBase.init(mandelbrot, writer, "Mandelbrot")
end)

Mandelbrot.KL = 64
Mandelbrot.RS = -2.2 * 4 / 3
Mandelbrot.RE = 0.5 * 4 / 3
Mandelbrot.IS = -1.35
Mandelbrot.IE = 1.35
Mandelbrot.XS = 256
Mandelbrot.YS = 256

function Mandelbrot:benchImplementation()
    local x, count, color = 0, 0, 0
    local dr, di = 0.0, 0.0
    local boost = 0
    local z = {r = 0.0, i = 0.0}
    local z2 = {r = 0.0, i = 0.0}
    local c = {r = 0.0, i = 0.0}

    Mandelbrot.dr = (Mandelbrot.RE - Mandelbrot.RS) / Mandelbrot.XS
    Mandelbrot.di = (Mandelbrot.IE - Mandelbrot.IS) / Mandelbrot.YS

    while not self:isCancelled() do
        for y = 0, Mandelbrot.YS - 1, 1 do
            x = Mandelbrot.XS - 1
            while x >= 0 do
                c.r = x * dr + Mandelbrot.RS
                c.i = y * di + Mandelbrot.IS
                z.i = 0.0
                z.r = 0.0

                for k = 0, Mandelbrot.KL - 1, 1 do
                    z2.r = z.r * z.r - z.i * z.i + c.r
                    z2.i = 2.0 * z.r * z.i + c.i
                    if (z2.r * z2.r + z2.i * z2.i > 4.0) then
                        color = rshift(k * 8, boost)
                        break
                    end
                    z = z2
                end

                x = x - 1
            end
        end

        count = count + 1
        boost = count % 20
    end

    return count / 0.012427 / 20
end
