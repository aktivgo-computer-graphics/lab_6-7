Uses crt, GraphABC;

const
  N = 500;

var
  X, Y, vX, vY: array[1..N] of integer;

var
  i: integer;

begin
  ClearWindow;
  for i := 1 to N do
  begin
    X[i] := 300 + i mod 25;  { начальные координаты осколков }
    Y[i] := 200 + i div 25;
    PutPixel(X[i], Y[i], clBlack);
    vX[i] := -10 + random(21); { составляющие скоростей осколков }
    vY[i] := -10 + random(21);
  end;
  
  for i := 1 to 300 do begin
    System.Console.Beep(36 + i, 2);
  end;
  
  REPEAT  { процесс разлета осколков }
    for i := 1 to N do begin 
      if (X[i] + vX[i] > 0) and(X[i] + vX[i] < WindowWidth) and (Y[i] + vY[i] > 0) and (Y[i] + vY[i] < WindowHeight)
    	  then begin  { если осколок еще не долетел до края }
        PutPixel(X[i],Y[i], clWhite);
        inc(X[i], vX[i]);
        inc(Y[i], vY[i]);
        PutPixel(X[i], Y[i], clRed);
        if (i > Round(N / 3)) and (vX[i] > 5) then
          dec(vX[i]);
        if (i > Round(N / 3)) and (vY[i] > 5) then
          dec(vY[i]);
      end { перерисовка в новом положении }
      else begin  { пиксель долетел до края - обнуляем его скорость и генерируем звук при "ударе" о край }
        vX[i] := 0;
        vY[i] := 0;
        System.Console.Beep(200, 2);
      end
    end;
  until KeyPressed; 
  readKey;
  ClearWindow;
end.