Uses crt, GraphABC;

const
  N = 500;

var
  X, Y: array[1..N] of integer;

var
  i, dx: integer;
  flag: integer;

begin
  ClearWindow;
  for i := 1 to N do
  begin
    X[i] := random(WindowWidth);
    Y[i] := random(WindowHeight - 200);
    PutPixel(X[i], Y[i], clNavy);
  end;
  Rectangle(100, WindowHeight - 100, WindowWidth - 100, WindowHeight);
  Line(WindowWidth - 100, WindowHeight - 100, WindowWidth, WindowHeight - 200);
  Line(100, WindowHeight - 100, 0, WindowHeight - 200);
  Line(Round(WindowWidth / 2), WindowHeight - 200, WindowWidth - 200, WindowHeight - 100);
  Line(Round(WindowWidth / 2), WindowHeight - 200, 200, WindowHeight - 100);
  repeat
    inc(flag);
    for i := 1 to N do
    begin
      // Ветер вправо
      if (flag < 500) then begin
        // Можем двигаться вправо вниз
        if (Y[i] < WindowHeight) and (X[i] < WindowWidth) and (GetPixel(X[i] + 1, Y[i] + 1) = GetPixel(Round(WindowWidth / 2), WindowHeight - 50)) then begin
          PutPixel(X[i], Y[i], clWhite);
          inc(X[i]);
          inc(Y[i]);
        end
        // Можем скатиться влево вниз
        else if (Y[i] < WindowHeight) and (X[i] > 0) and (GetPixel(X[i] - 1, Y[i] + 1) = GetPixel(Round(WindowWidth / 2), WindowHeight - 50)) then begin
            PutPixel(X[i], Y[i], clWhite);
            dec(X[i]);
            inc(Y[i]);
          end
        // Легли на ровную поверхность
        else begin
          X[i] := random(WindowWidth);
          Y[i] := random((WindowHeight - 100) div 10);
        end;
        PutPixel(X[i], Y[i], clNavy);
      end
      // Ветер влево
      else begin
        // Можем двигаться влево вниз
        if (Y[i] < WindowHeight) and (X[i] > 0) and (GetPixel(X[i] - 1, Y[i] + 1) = GetPixel(Round(WindowWidth / 2), WindowHeight - 50)) then begin
          PutPixel(X[i], Y[i], clWhite);
          dec(X[i]);
          inc(Y[i]);
        end
        // Можем скатиться вправо вниз
        else if (Y[i] < WindowHeight) and (X[i] < WindowWidth) and (GetPixel(X[i] + 1, Y[i] + 1) = GetPixel(Round(WindowWidth / 2), WindowHeight - 50)) then begin
            PutPixel(X[i], Y[i], clWhite);
            inc(X[i]);
            inc(Y[i]);
          end
        // Легли на ровную поверхность
        else begin
          X[i] := random(WindowWidth);
          Y[i] := random((WindowHeight - 100) div 10);
        end;
        PutPixel(X[i], Y[i], clNavy);
      end;
    end;
    if (flag > 1000) then
      flag := 0;
  until KeyPressed; 
  readKey;
  ClearWindow;
end.