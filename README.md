# Эмулятор гит 
Девочка Василиса захотела написать свой гит. 
Для начала, она решила, что будет следить только за ограниченным числом файлов. 
Их количество она задаст при инициализации класса. В каждом файле будет храниться одно целое число типа int, а обращаться к файлам можно будет по номерам.

Был реализован нём класс Git со следующим интерфейсом:

`public Git(int filesCount)` — инициализирует структуру, которая может следить за filesCount файлами

`void Update(int fileNumber, int value)` — меняет содержимое файла fileNumber на value

`int Commit()` — фиксирует текущее состояние файлов, возвращает commitNumber: число раз, которое был вызван Commit() минус 1

`int Checkout(int commitNumber, int fileNumber)` — возвращает содержимое файла fileNumber на момент коммита commitNumber. Если этого коммита ещё не было — бросает ArgumentException
