# AutomatoElectations
Проект по учебе
Проект был выдан по индивудальным заданиям
## Требования к проекту
+ Русскоязычный интерфейс
+ Использование WPF
+ Добавление, редактирование и измение данных кандидата
+ Добавление, редактирование и измение данных избирателя
+ Для упрощения работы необходимы сортировка данных и поиск в таблицах

### Для реализации приложения был выбран стек  Microsoft SQL Server + WPF

## Разработка базы данных

Диаграмма базы данных

![image](https://user-images.githubusercontent.com/78948171/163378696-3ab87831-2666-4410-86b3-226e0c2e5b74.png)

## Окно авторизации

Здесь ползаватель входит в систему, так же реализована валидация при входе

![image](https://user-images.githubusercontent.com/78948171/163380508-df5443a3-fc47-428a-a37e-81c5e9f9552d.png)
![image](https://user-images.githubusercontent.com/78948171/163380653-26d0ceb6-ee6b-40ba-8dfb-5ec99ee5ba2f.png)

## Функционал администратора
При входе с аккауната администратора пользователь попадает на страницу со списком избирателей

![image](https://user-images.githubusercontent.com/78948171/163381344-6888361b-9011-48ca-a6f6-685d678a1386.png)

Администратору доступны функции
+ Добавления изменения и удаления Избирателей
+ Добавления изменения и удаления Кандидаов
+ Добавления изменения и удаления Партий

![image](https://user-images.githubusercontent.com/78948171/163382338-6cd2b8bc-56a2-4e23-b990-1ee54245de07.png)
![image](https://user-images.githubusercontent.com/78948171/163382990-c8412ea0-4999-458b-b128-970cc46d6261.png)

Добавление нового избирателя

![image](https://user-images.githubusercontent.com/78948171/163383201-ef95e9a2-38db-48aa-af94-c232fa7a2c0f.png)

Добавление паспортных данных избирателя

![image](https://user-images.githubusercontent.com/78948171/163383257-fce5946c-57aa-4e0c-a6b6-37665e3de934.png)

Так же все данные можно изменить 

![image](https://user-images.githubusercontent.com/78948171/163383417-6bf36241-9035-418a-92c4-49ee44a9cd67.png)

Поиск

![image](https://user-images.githubusercontent.com/78948171/163383584-9b269074-5ab2-4a36-a6f8-2f38f5dba996.png)

## Функционал администратора

При входе с аккаунта избирателя пользователь видит список кандидатов за которых можно голосовать

В этом списке так же доступна сортировка и поиск кандидатов

Окно голосования

![image](https://user-images.githubusercontent.com/78948171/163384309-761f17ca-f0cf-4c5d-981f-190660fdcd8d.png)

После того как пользователь проголосовал он может только просматривать список кандидатов и партий

![image](https://user-images.githubusercontent.com/78948171/163384564-8e4439ed-d2ac-446f-b041-8eadef4768e4.png)

## Будующие исправления:
+ Исправление интерфейса
+ Добавление окна просмотра результатов
+ Рефакторинг кода
