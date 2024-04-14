CREATE TABLE [users] (
  [id] integer PRIMARY KEY,
  username varchar(255),
  name varchar(255),
  password varchar(255),
  degree_id integer
);

CREATE TABLE mycourses (
  id integer PRIMARY KEY,
  user_id integer,
  course_id integer
);

CREATE TABLE courses (
  id integer PRIMARY KEY,
  code varchar(255),
  name varchar(255),
  credit integer
);

CREATE TABLE degrees (
  id integer PRIMARY KEY,
  name varchar(255)
);

CREATE TABLE approved_degress (
  id integer PRIMARY KEY,
  course_id integer,
  degree_id integer
);

CREATE TABLE events (
  id integer PRIMARY KEY,
  course_id integer,
  name varchar(255),
  description varchar(255)
);

ALTER TABLE events ADD FOREIGN KEY (course_id) REFERENCES courses (id);

ALTER TABLE approved_degress ADD FOREIGN KEY (degree_id) REFERENCES degrees (id);

ALTER TABLE approved_degress ADD FOREIGN KEY (course_id) REFERENCES courses (id);

ALTER TABLE users ADD FOREIGN KEY (degree_id) REFERENCES degrees (id);

ALTER TABLE mycourses ADD FOREIGN KEY (user_id) REFERENCES users (id);

ALTER TABLE mycourses ADD FOREIGN KEY (course_id) REFERENCES courses (id);
