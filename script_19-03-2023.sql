--
-- PostgreSQL database dump
--

-- Dumped from database version 13.3 (Debian 13.3-1.pgdg100+1)
-- Dumped by pg_dump version 15.2

-- Started on 2023-03-27 11:12:17

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3057 (class 1262 OID 16384)
-- Name: CatsShop; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "CatsShop" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE "CatsShop" OWNER TO postgres;

\connect "CatsShop"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

-- *not* creating schema, since initdb creates it


ALTER SCHEMA public OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 219 (class 1259 OID 24792)
-- Name: Buy; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Buy" (
    "BuyID" integer NOT NULL,
    "BuyClientID" integer NOT NULL,
    "BuyPozitionID" integer NOT NULL,
    "BuyDate" timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."Buy" OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 16397)
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    "UserID" integer NOT NULL,
    "UserLogin" character varying(100) NOT NULL,
    "UserPassword" character varying(100) NOT NULL,
    "UserRoleID" integer DEFAULT 1 NOT NULL
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 24835)
-- Name: BuyUser; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public."BuyUser" AS
 SELECT "Buy"."BuyID",
    "Buy"."BuyClientID",
    "Buy"."BuyPozitionID",
    "Buy"."BuyDate",
    "User"."UserID",
    "User"."UserLogin",
    "User"."UserPassword",
    "User"."UserRoleID"
   FROM public."Buy",
    public."User"
  WHERE ("Buy"."BuyClientID" = "User"."UserID");


ALTER TABLE public."BuyUser" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 24790)
-- Name: Buy_BuyID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Buy" ALTER COLUMN "BuyID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Buy_BuyID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 205 (class 1259 OID 16412)
-- Name: Cat; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Cat" (
    "CatID" integer NOT NULL,
    "CatModelID" integer NOT NULL,
    "CatAge" integer NOT NULL
);


ALTER TABLE public."Cat" OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 16419)
-- Name: CatColor; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."CatColor" (
    "CatColorID" integer NOT NULL,
    "CatColorName" character varying(100) NOT NULL
);


ALTER TABLE public."CatColor" OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 16417)
-- Name: CatColor_CatColorID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."CatColor" ALTER COLUMN "CatColorID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."CatColor_CatColorID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 209 (class 1259 OID 16426)
-- Name: CatGender; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."CatGender" (
    "CatGenderID" integer NOT NULL,
    "CatGenderName" character(1) NOT NULL
);


ALTER TABLE public."CatGender" OWNER TO postgres;

--
-- TOC entry 208 (class 1259 OID 16424)
-- Name: CatGender_CatGenderID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."CatGender" ALTER COLUMN "CatGenderID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."CatGender_CatGenderID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 213 (class 1259 OID 16440)
-- Name: CatModel; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."CatModel" (
    "CatModelID" integer NOT NULL,
    "CatColorID" integer NOT NULL,
    "CatGenderID" integer NOT NULL,
    "CatSpeciesID" integer NOT NULL
);


ALTER TABLE public."CatModel" OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 16438)
-- Name: CatModel_CatModelID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."CatModel" ALTER COLUMN "CatModelID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."CatModel_CatModelID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 211 (class 1259 OID 16433)
-- Name: CatSpecies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."CatSpecies" (
    "CatSpeciesID" integer NOT NULL,
    "CatSpeciesName" character varying(100) NOT NULL
);


ALTER TABLE public."CatSpecies" OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 16431)
-- Name: CatSpecies_CatSpeciesID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."CatSpecies" ALTER COLUMN "CatSpeciesID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."CatSpecies_CatSpeciesID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 204 (class 1259 OID 16410)
-- Name: Cat_CatID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Cat" ALTER COLUMN "CatID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Cat_CatID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 215 (class 1259 OID 16472)
-- Name: Pozition; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Pozition" (
    "PozitionID" integer NOT NULL,
    "PozitionCatID" integer NOT NULL,
    "PozitionDateAdded" timestamp without time zone DEFAULT now() NOT NULL,
    "PozitionDateOfChanged" timestamp without time zone DEFAULT now() NOT NULL,
    "PozitionCost" money DEFAULT 100 NOT NULL
);


ALTER TABLE public."Pozition" OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16470)
-- Name: Pozition_PozitionID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Pozition" ALTER COLUMN "PozitionID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Pozition_PozitionID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 201 (class 1259 OID 16387)
-- Name: Role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Role" (
    "RoleID" integer NOT NULL,
    "RoleName" character varying(100)
);


ALTER TABLE public."Role" OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 16385)
-- Name: Role_RoleID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Role" ALTER COLUMN "RoleID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Role_RoleID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 216 (class 1259 OID 16485)
-- Name: Session; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Session" (
    "SessionKey" character(20) NOT NULL,
    "SessionUserID" integer NOT NULL,
    "TimeOpen" timestamp with time zone DEFAULT now()
);


ALTER TABLE public."Session" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16499)
-- Name: UserRole; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public."UserRole" AS
 SELECT "User"."UserID" AS "ID",
    "Role"."RoleID",
    "Role"."RoleName" AS "Role",
    "User"."UserLogin" AS "Login",
    "User"."UserPassword" AS "Password"
   FROM public."Role",
    public."User"
  WHERE ("Role"."RoleID" = "User"."UserRoleID");


ALTER TABLE public."UserRole" OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 16395)
-- Name: User_UserID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."User" ALTER COLUMN "UserID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."User_UserID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 3051 (class 0 OID 24792)
-- Dependencies: 219
-- Data for Name: Buy; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Buy" ("BuyID", "BuyClientID", "BuyPozitionID", "BuyDate") OVERRIDING SYSTEM VALUE VALUES (1, 1, 1, '2023-03-22 00:00:00');
INSERT INTO public."Buy" ("BuyID", "BuyClientID", "BuyPozitionID", "BuyDate") OVERRIDING SYSTEM VALUE VALUES (2, 1, 3, '2023-03-22 00:00:00');
INSERT INTO public."Buy" ("BuyID", "BuyClientID", "BuyPozitionID", "BuyDate") OVERRIDING SYSTEM VALUE VALUES (3, 5, 4, '2023-03-22 00:00:00');
INSERT INTO public."Buy" ("BuyID", "BuyClientID", "BuyPozitionID", "BuyDate") OVERRIDING SYSTEM VALUE VALUES (4, 1, 3, '2023-03-23 00:00:00');


--
-- TOC entry 3038 (class 0 OID 16412)
-- Dependencies: 205
-- Data for Name: Cat; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Cat" ("CatID", "CatModelID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (1, 2, 10);
INSERT INTO public."Cat" ("CatID", "CatModelID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (2, 2, 10);
INSERT INTO public."Cat" ("CatID", "CatModelID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (4, 4, 15);
INSERT INTO public."Cat" ("CatID", "CatModelID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (5, 4, 8);
INSERT INTO public."Cat" ("CatID", "CatModelID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (6, 4, 5);
INSERT INTO public."Cat" ("CatID", "CatModelID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (8, 5, 11);
INSERT INTO public."Cat" ("CatID", "CatModelID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (9, 5, 18);
INSERT INTO public."Cat" ("CatID", "CatModelID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (10, 4, 20);
INSERT INTO public."Cat" ("CatID", "CatModelID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (3, 2, 16);


--
-- TOC entry 3040 (class 0 OID 16419)
-- Dependencies: 207
-- Data for Name: CatColor; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (2, 'Красный');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (3, 'Оранжевый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (4, 'Жёлтый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (5, 'Зелёный');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (6, 'Голубой');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (7, 'Синий');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (8, 'Фиолетовый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (9, 'Пурпурный');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (10, 'Розовый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (11, 'Коричневый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (12, 'Чёрный');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (13, 'Серый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (14, 'Белый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (15, 'Бежевый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (16, 'Каштановый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (17, 'Бирюзовый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (18, 'Салатовый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (19, 'Каралловый');
INSERT INTO public."CatColor" ("CatColorID", "CatColorName") OVERRIDING SYSTEM VALUE VALUES (20, 'Малиновый');


--
-- TOC entry 3042 (class 0 OID 16426)
-- Dependencies: 209
-- Data for Name: CatGender; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."CatGender" ("CatGenderID", "CatGenderName") OVERRIDING SYSTEM VALUE VALUES (9, 'м');
INSERT INTO public."CatGender" ("CatGenderID", "CatGenderName") OVERRIDING SYSTEM VALUE VALUES (10, 'ж');


--
-- TOC entry 3046 (class 0 OID 16440)
-- Dependencies: 213
-- Data for Name: CatModel; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."CatModel" ("CatModelID", "CatColorID", "CatGenderID", "CatSpeciesID") OVERRIDING SYSTEM VALUE VALUES (1, 2, 9, 1);
INSERT INTO public."CatModel" ("CatModelID", "CatColorID", "CatGenderID", "CatSpeciesID") OVERRIDING SYSTEM VALUE VALUES (2, 2, 10, 1);
INSERT INTO public."CatModel" ("CatModelID", "CatColorID", "CatGenderID", "CatSpeciesID") OVERRIDING SYSTEM VALUE VALUES (4, 4, 9, 1);
INSERT INTO public."CatModel" ("CatModelID", "CatColorID", "CatGenderID", "CatSpeciesID") OVERRIDING SYSTEM VALUE VALUES (5, 4, 9, 3);
INSERT INTO public."CatModel" ("CatModelID", "CatColorID", "CatGenderID", "CatSpeciesID") OVERRIDING SYSTEM VALUE VALUES (6, 9, 9, 3);
INSERT INTO public."CatModel" ("CatModelID", "CatColorID", "CatGenderID", "CatSpeciesID") OVERRIDING SYSTEM VALUE VALUES (7, 14, 9, 3);
INSERT INTO public."CatModel" ("CatModelID", "CatColorID", "CatGenderID", "CatSpeciesID") OVERRIDING SYSTEM VALUE VALUES (8, 5, 10, 2);
INSERT INTO public."CatModel" ("CatModelID", "CatColorID", "CatGenderID", "CatSpeciesID") OVERRIDING SYSTEM VALUE VALUES (9, 4, 10, 4);


--
-- TOC entry 3044 (class 0 OID 16433)
-- Dependencies: 211
-- Data for Name: CatSpecies; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."CatSpecies" ("CatSpeciesID", "CatSpeciesName") OVERRIDING SYSTEM VALUE VALUES (1, 'Американский кёрл');
INSERT INTO public."CatSpecies" ("CatSpeciesID", "CatSpeciesName") OVERRIDING SYSTEM VALUE VALUES (2, 'Бомбейская кошка');
INSERT INTO public."CatSpecies" ("CatSpeciesID", "CatSpeciesName") OVERRIDING SYSTEM VALUE VALUES (3, 'Тойгер');
INSERT INTO public."CatSpecies" ("CatSpeciesID", "CatSpeciesName") OVERRIDING SYSTEM VALUE VALUES (4, 'Бурмилла');


--
-- TOC entry 3048 (class 0 OID 16472)
-- Dependencies: 215
-- Data for Name: Pozition; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Pozition" ("PozitionID", "PozitionCatID", "PozitionDateAdded", "PozitionDateOfChanged", "PozitionCost") OVERRIDING SYSTEM VALUE VALUES (2, 3, '2023-03-17 00:00:00', '2023-03-17 00:00:00', '$150.00');
INSERT INTO public."Pozition" ("PozitionID", "PozitionCatID", "PozitionDateAdded", "PozitionDateOfChanged", "PozitionCost") OVERRIDING SYSTEM VALUE VALUES (3, 3, '2023-03-17 00:00:00', '2023-03-17 00:00:00', '$180.40');
INSERT INTO public."Pozition" ("PozitionID", "PozitionCatID", "PozitionDateAdded", "PozitionDateOfChanged", "PozitionCost") OVERRIDING SYSTEM VALUE VALUES (4, 1, '2023-03-17 00:00:00', '2023-03-17 00:00:00', '$180.40');
INSERT INTO public."Pozition" ("PozitionID", "PozitionCatID", "PozitionDateAdded", "PozitionDateOfChanged", "PozitionCost") OVERRIDING SYSTEM VALUE VALUES (6, 9, '2023-03-20 00:00:00', '2023-03-20 00:00:00', '$500.00');
INSERT INTO public."Pozition" ("PozitionID", "PozitionCatID", "PozitionDateAdded", "PozitionDateOfChanged", "PozitionCost") OVERRIDING SYSTEM VALUE VALUES (1, 2, '2023-03-17 00:00:00', '2023-03-20 00:00:00', '$100.00');
INSERT INTO public."Pozition" ("PozitionID", "PozitionCatID", "PozitionDateAdded", "PozitionDateOfChanged", "PozitionCost") OVERRIDING SYSTEM VALUE VALUES (7, 3, '2023-03-23 00:00:00', '2023-03-23 00:00:00', '$400.00');


--
-- TOC entry 3034 (class 0 OID 16387)
-- Dependencies: 201
-- Data for Name: Role; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Role" ("RoleID", "RoleName") OVERRIDING SYSTEM VALUE VALUES (1, 'Клиент');
INSERT INTO public."Role" ("RoleID", "RoleName") OVERRIDING SYSTEM VALUE VALUES (2, 'Администратор');


--
-- TOC entry 3049 (class 0 OID 16485)
-- Dependencies: 216
-- Data for Name: Session; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Session" ("SessionKey", "SessionUserID", "TimeOpen") VALUES ('69287973182607806794', 1, '2023-03-26 20:48:05.341334+00');
INSERT INTO public."Session" ("SessionKey", "SessionUserID", "TimeOpen") VALUES ('45472753098195710228', 1, '2023-03-26 20:48:08.087528+00');
INSERT INTO public."Session" ("SessionKey", "SessionUserID", "TimeOpen") VALUES ('18176008533382219241', 1, '2023-03-26 20:48:09.010505+00');
INSERT INTO public."Session" ("SessionKey", "SessionUserID", "TimeOpen") VALUES ('53795433989900623753', 10, '2023-03-26 20:51:14.931759+00');


--
-- TOC entry 3036 (class 0 OID 16397)
-- Dependencies: 203
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (5, 'anton2', '123', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (6, 'anton3', '123', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (7, 'anton4', 'password', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (3, 'anton1', 'password', 2);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (11, 'user1', 'password', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (13, '123', '123', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (14, '12345', '12345', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (10, 'user', 'password', 2);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (16, 'AntonSidorov', '1234', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (1, 'anton', 'password', 1);


--
-- TOC entry 3059 (class 0 OID 0)
-- Dependencies: 218
-- Name: Buy_BuyID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Buy_BuyID_seq"', 4, true);


--
-- TOC entry 3060 (class 0 OID 0)
-- Dependencies: 206
-- Name: CatColor_CatColorID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."CatColor_CatColorID_seq"', 21, true);


--
-- TOC entry 3061 (class 0 OID 0)
-- Dependencies: 208
-- Name: CatGender_CatGenderID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."CatGender_CatGenderID_seq"', 10, true);


--
-- TOC entry 3062 (class 0 OID 0)
-- Dependencies: 212
-- Name: CatModel_CatModelID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."CatModel_CatModelID_seq"', 12, true);


--
-- TOC entry 3063 (class 0 OID 0)
-- Dependencies: 210
-- Name: CatSpecies_CatSpeciesID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."CatSpecies_CatSpeciesID_seq"', 7, true);


--
-- TOC entry 3064 (class 0 OID 0)
-- Dependencies: 204
-- Name: Cat_CatID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Cat_CatID_seq"', 13, true);


--
-- TOC entry 3065 (class 0 OID 0)
-- Dependencies: 214
-- Name: Pozition_PozitionID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Pozition_PozitionID_seq"', 7, true);


--
-- TOC entry 3066 (class 0 OID 0)
-- Dependencies: 200
-- Name: Role_RoleID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Role_RoleID_seq"', 3, true);


--
-- TOC entry 3067 (class 0 OID 0)
-- Dependencies: 202
-- Name: User_UserID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_UserID_seq"', 20, true);


--
-- TOC entry 2891 (class 2606 OID 24797)
-- Name: Buy BuyPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Buy"
    ADD CONSTRAINT "BuyPK" PRIMARY KEY ("BuyID");


--
-- TOC entry 2879 (class 2606 OID 16423)
-- Name: CatColor CatColorPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CatColor"
    ADD CONSTRAINT "CatColorPK" PRIMARY KEY ("CatColorID");


--
-- TOC entry 2881 (class 2606 OID 16430)
-- Name: CatGender CatGenderPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CatGender"
    ADD CONSTRAINT "CatGenderPK" PRIMARY KEY ("CatGenderID");


--
-- TOC entry 2885 (class 2606 OID 16444)
-- Name: CatModel CatModelPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CatModel"
    ADD CONSTRAINT "CatModelPK" PRIMARY KEY ("CatModelID");


--
-- TOC entry 2877 (class 2606 OID 16416)
-- Name: Cat CatPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cat"
    ADD CONSTRAINT "CatPK" PRIMARY KEY ("CatID");


--
-- TOC entry 2883 (class 2606 OID 16437)
-- Name: CatSpecies CatSpeciesPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CatSpecies"
    ADD CONSTRAINT "CatSpeciesPK" PRIMARY KEY ("CatSpeciesID");


--
-- TOC entry 2887 (class 2606 OID 16477)
-- Name: Pozition PozitionPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pozition"
    ADD CONSTRAINT "PozitionPK" PRIMARY KEY ("PozitionID");


--
-- TOC entry 2871 (class 2606 OID 16394)
-- Name: Role RolePK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Role"
    ADD CONSTRAINT "RolePK" PRIMARY KEY ("RoleID");


--
-- TOC entry 2889 (class 2606 OID 16489)
-- Name: Session SessionPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Session"
    ADD CONSTRAINT "SessionPK" PRIMARY KEY ("SessionKey");


--
-- TOC entry 2873 (class 2606 OID 16402)
-- Name: User UserPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "UserPK" PRIMARY KEY ("UserID");


--
-- TOC entry 2875 (class 2606 OID 16404)
-- Name: User UserUK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "UserUK" UNIQUE ("UserLogin");


--
-- TOC entry 2899 (class 2606 OID 24798)
-- Name: Buy BuyClientConstraint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Buy"
    ADD CONSTRAINT "BuyClientConstraint" FOREIGN KEY ("BuyClientID") REFERENCES public."User"("UserID") ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 2900 (class 2606 OID 24803)
-- Name: Buy BuyPozitionID; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Buy"
    ADD CONSTRAINT "BuyPozitionID" FOREIGN KEY ("BuyPozitionID") REFERENCES public."Pozition"("PozitionID") ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 2894 (class 2606 OID 16445)
-- Name: CatModel CatColorConstraint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CatModel"
    ADD CONSTRAINT "CatColorConstraint" FOREIGN KEY ("CatColorID") REFERENCES public."CatColor"("CatColorID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2897 (class 2606 OID 16478)
-- Name: Pozition CatConstraint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pozition"
    ADD CONSTRAINT "CatConstraint" FOREIGN KEY ("PozitionCatID") REFERENCES public."Cat"("CatID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2893 (class 2606 OID 16460)
-- Name: Cat CatModelConstraint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cat"
    ADD CONSTRAINT "CatModelConstraint" FOREIGN KEY ("CatModelID") REFERENCES public."CatModel"("CatModelID") ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 2895 (class 2606 OID 16455)
-- Name: CatModel CatSpeciesConstraint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CatModel"
    ADD CONSTRAINT "CatSpeciesConstraint" FOREIGN KEY ("CatSpeciesID") REFERENCES public."CatSpecies"("CatSpeciesID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2898 (class 2606 OID 16490)
-- Name: Session UserConstraint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Session"
    ADD CONSTRAINT "UserConstraint" FOREIGN KEY ("SessionUserID") REFERENCES public."User"("UserID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2892 (class 2606 OID 16405)
-- Name: User UserRole; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "UserRole" FOREIGN KEY ("UserRoleID") REFERENCES public."Role"("RoleID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2896 (class 2606 OID 16450)
-- Name: CatModel catGenderConstraint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CatModel"
    ADD CONSTRAINT "catGenderConstraint" FOREIGN KEY ("CatGenderID") REFERENCES public."CatGender"("CatGenderID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 3058 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2023-03-27 11:12:18

--
-- PostgreSQL database dump complete
--

