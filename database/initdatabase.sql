drop table USER_SEAT;
drop table TIME_DIFFERENCE;
drop table FEEDBACK_ANSWER;
drop table FEEDBACK;
drop table MESSAGE;
drop table SEAT;
drop table CHANGES;
drop table CANCELS;
drop table PURCHASES;
drop table PLANE_TICKET;
drop table ADMIN_ACCOUNT;
drop table USER_ACCOUNT;
drop table ARRIVES;
drop table DEPARTS;
drop table AIRPORT;
drop table CITY;
drop table FLIGHT;
drop table AIRPLANE;
drop table AIRLINE;
drop table AIRLINE_COMPANY;

-- 航线表    
create table airline
    (airline_ID number(6),
     mileage number(6),
     airline_name varchar2(40),
     primary key(airline_ID)
    );

-- 航空公司表
create table airline_company
    (company_ID varchar2(3),
     company_name varchar2(20),
     primary key (company_ID)
    );


-- 飞机表
create table airplane
    (airplane_ID number(6),
     company_ID varchar(3),
     airplane_state number(1),
     model_no varchar2(10),
     primary key (airplane_ID),
     foreign key (company_ID) references airline_company on delete cascade
    );

-- 航班表
create table flight
    (flight_number varchar2(6),
     airplane_ID number(6),
     depart_date date,
     arrive_date date,
     flight_state varchar2(10),
     airline_ID number(6),
     company_name varchar2(20),
     first_class_price number(6),
     economy_class_price number(6),
     primary key(flight_number, depart_date),
     foreign key(airplane_ID) references airplane on delete cascade,
     foreign key(airline_ID) references airline on delete cascade
    );
    
-- 城市表
create table city
    (city_ID varchar2(4),
     city_name varchar2(20),
     country varchar2(20),
     COV19_risk char(1),
     primary key(city_ID)
    );
     
-- 机场表
create table airport
    (airport_ID varchar2(3),
     city_ID varchar(4),
     airport_name varchar2(40),
     foreign key(city_ID) references city on delete cascade,
     primary key(airport_ID)
    );

-- 出发表
create table departs
    (airline_ID number(6),
    airport_ID varchar2(3),
     primary key(airline_ID),
     foreign key(airport_ID) references airport on delete cascade,
     foreign key(airline_ID) references airline on delete cascade
    );
    
-- 到达表
create table arrives
    (airline_ID number(6),
     airport_ID varchar2(3),
     primary key(airline_ID),
     foreign key(airport_ID) references airport on delete cascade,
     foreign key(airline_ID) references airline on delete cascade
    );    

-- 用户表
create table user_account
    (user_ID varchar2(20),
     user_name varchar2(20),
     user_password varchar2(20),
     phone_number number(20),
     user_email varchar2(20),
     user_idcard varchar2(30),
     primary key (user_ID)
    );

-- 管理员表
create table admin_account
    (admin_ID varchar2(6),
     admin_state number(1),
     admin_password varchar2(6),
     primary key(admin_ID)
    );

-- 机票
create table plane_ticket
    (ticket_ID varchar2(32),
     flight_number varchar2(6),
     flight_date date,
     price number(6),
     flight_class varchar2(10),
     passenger_name varchar2(10),
     ticket_state varchar2(8),
     primary key (ticket_ID),
     foreign key (flight_number, flight_date)  references flight on delete cascade);

-- 购买表
create table purchases
    (user_ID varchar2(20),
     purchase_time timestamp,
     ticket_ID varchar2(32),
     primary key(ticket_ID),
     foreign key(user_ID) references user_account on delete cascade,
     foreign key(ticket_ID) references plane_ticket on delete cascade 
    );


-- 取消表
create table cancels
    (cancels_time timestamp,
     ticket_ID varchar2(32),
     user_ID varchar2(20),
     cancels_surcharge number(6),
     primary key(ticket_ID),
     foreign key(ticket_ID) references plane_ticket on delete cascade,
     foreign key(user_ID) references user_account on delete cascade
    );

-- 改签表
create table changes
    (changes_time timestamp,
     ticket_ID varchar2(32),
     user_ID varchar2(20),
     changes_surcharge number(6),
     primary key(ticket_ID),
     foreign key(ticket_ID) references plane_ticket on delete cascade,
     foreign key(user_ID) references user_account on delete cascade
    );
      
-- 舱位表
create table seat
     (airplane_ID number(6),
      flight_class varchar2(20),
      row_sum number(3),
      column_sum number(3),
      primary key (airplane_ID, flight_class),
      foreign key (airplane_ID)  references airplane on delete cascade);  

-- 消息提示表
create table message
    (message_ID varchar2(32),
     user_ID varchar2(20),
     message_content varchar2(500),
     message_time date,
     primary key(message_ID),
     foreign key(user_ID) references user_account on delete cascade
    );
 
   
-- 反馈表
create table feedback
    (user_ID varchar2(20),
     feedback_ID varchar2(32),
     feedback_time timestamp,
     feedback_content varchar2(500),
     primary key (feedback_ID),
     foreign key (user_ID) references user_account on delete cascade);   
   
   
-- 反馈处理表    
create table feedback_answer
    (admin_ID varchar2(6),
     feedback_ID varchar2(32),
     dealt_ID varchar2(32),
     answer_content varchar2(500),
     answer_time timestamp,
     primary key (dealt_ID),
     foreign key (feedback_ID) references feedback on delete cascade,
     foreign key (admin_ID) references admin_account on delete cascade);


-- 时差表
create table time_difference
    (country varchar2(20),
     city_name varchar2(20),
     time_difference number(3),
     primary key(country, city_name));

-- 用户座位表
create table user_seat
    (ticket_ID varchar2(32),
     seat_row number(2),
     seat_col varchar2(1),
     primary key(ticket_ID),
     foreign key(ticket_ID) references plane_ticket on delete cascade);

--触发器
CREATE OR REPLACE TRIGGER nameChange
AFTER update OF USER_NAME
ON USER_ACCOUNT
FOR EACH ROW
BEGIN
 UPDATE PLANE_TICKET SET PASSENGER_NAME = :new.USER_NAME
 WHERE PASSENGER_NAME = :old.USER_NAME;
END;

CREATE OR REPLACE TRIGGER deleteUserSeat
AFTER insert on cancels
FOR EACH ROW
BEGIN
 DELETE FROM USER_SEAT WHERE TICKET_ID = :new.TICKET_ID;
END;


CREATE OR REPLACE TRIGGER cDeleteUserSeat
AFTER insert on changes
FOR EACH ROW
BEGIN
 DELETE FROM USER_SEAT WHERE TICKET_ID = :new.TICKET_ID;
END;