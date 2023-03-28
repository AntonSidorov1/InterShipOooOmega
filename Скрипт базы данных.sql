--
-- PostgreSQL database dump
--

-- Dumped from database version 13.3 (Debian 13.3-1.pgdg100+1)
-- Dumped by pg_dump version 15.2

-- Started on 2023-03-28 22:31:35

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
-- TOC entry 3005 (class 1262 OID 24839)
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
-- TOC entry 207 (class 1259 OID 24876)
-- Name: Cat; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Cat" (
    "CatID" integer NOT NULL,
    "CatColor" character varying(100) NOT NULL,
    "CatSpecies" character varying(100) NOT NULL,
    "CatGenderID" integer NOT NULL,
    "CatAge" integer NOT NULL
);


ALTER TABLE public."Cat" OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 24869)
-- Name: Gender; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Gender" (
    "GenderID" integer NOT NULL,
    "GenderName" character varying(1) NOT NULL
);


ALTER TABLE public."Gender" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 24867)
-- Name: CatGender_CatGenderID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Gender" ALTER COLUMN "GenderID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."CatGender_CatGenderID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
    CYCLE
);


--
-- TOC entry 209 (class 1259 OID 24904)
-- Name: Pozition; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Pozition" (
    "PozitionID" integer NOT NULL,
    "PozitionCatID" integer NOT NULL,
    "PozitionPrice" money DEFAULT 100 NOT NULL,
    "PozitionDateAdded" timestamp without time zone DEFAULT now() NOT NULL,
    "PozitionDateChanged" timestamp without time zone DEFAULT now() NOT NULL,
    "PozitionBought" boolean DEFAULT false NOT NULL
);


ALTER TABLE public."Pozition" OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 24928)
-- Name: CatPozition; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public."CatPozition" AS
 SELECT "Pozition"."PozitionCatID" AS "ID",
    "Pozition"."PozitionPrice" AS "Price",
    "Pozition"."PozitionDateAdded" AS "DateAdded",
    "Pozition"."PozitionDateChanged" AS "DateChanged",
    "Pozition"."PozitionBought" AS "Bought",
    "Cat"."CatColor" AS "Color",
    "Cat"."CatSpecies" AS "Species",
    "Cat"."CatAge" AS "Age",
    "Gender"."GenderName" AS "Gender"
   FROM public."Pozition",
    public."Cat",
    public."Gender"
  WHERE (("Pozition"."PozitionCatID" = "Cat"."CatID") AND ("Cat"."CatGenderID" = "Gender"."GenderID"));


ALTER TABLE public."CatPozition" OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 24874)
-- Name: Cat_catID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Cat" ALTER COLUMN "CatID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Cat_catID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
    CYCLE
);


--
-- TOC entry 208 (class 1259 OID 24902)
-- Name: Pozition_PozitionID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Pozition" ALTER COLUMN "PozitionID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Pozition_PozitionID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
    CYCLE
);


--
-- TOC entry 201 (class 1259 OID 24842)
-- Name: Role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Role" (
    "RoleID" integer NOT NULL,
    "RoleName" character varying(100) NOT NULL
);


ALTER TABLE public."Role" OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 24840)
-- Name: Role_RoleID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Role" ALTER COLUMN "RoleID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Role_RoleID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
    CYCLE
);


--
-- TOC entry 203 (class 1259 OID 24851)
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    "UserID" integer NOT NULL,
    "UserLogin" character varying(100) NOT NULL,
    "UserPassword" character varying(100) NOT NULL,
    "UserRoleID" integer NOT NULL
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 24939)
-- Name: UserRole; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public."UserRole" AS
 SELECT "User"."UserID" AS "ID",
    "User"."UserLogin" AS "Login",
    "User"."UserPassword" AS "Password",
    "Role"."RoleID",
    "Role"."RoleName" AS "Role"
   FROM public."User",
    public."Role"
  WHERE ("User"."UserRoleID" = "Role"."RoleID");


ALTER TABLE public."UserRole" OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 24849)
-- Name: User_UserID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."User" ALTER COLUMN "UserID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."User_UserID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
    CYCLE
);


--
-- TOC entry 2997 (class 0 OID 24876)
-- Dependencies: 207
-- Data for Name: Cat; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Cat" ("CatID", "CatColor", "CatSpecies", "CatGenderID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (1, 'Красный', 'Тойгер', 1, 12);
INSERT INTO public."Cat" ("CatID", "CatColor", "CatSpecies", "CatGenderID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (2, 'Розовый', 'Тойгер', 1, 12);
INSERT INTO public."Cat" ("CatID", "CatColor", "CatSpecies", "CatGenderID", "CatAge") OVERRIDING SYSTEM VALUE VALUES (3, 'Розовый', 'Тойгер', 2, 5);


--
-- TOC entry 2995 (class 0 OID 24869)
-- Dependencies: 205
-- Data for Name: Gender; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Gender" ("GenderID", "GenderName") OVERRIDING SYSTEM VALUE VALUES (1, 'ж');
INSERT INTO public."Gender" ("GenderID", "GenderName") OVERRIDING SYSTEM VALUE VALUES (2, 'м');


--
-- TOC entry 2999 (class 0 OID 24904)
-- Dependencies: 209
-- Data for Name: Pozition; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Pozition" ("PozitionID", "PozitionCatID", "PozitionPrice", "PozitionDateAdded", "PozitionDateChanged", "PozitionBought") OVERRIDING SYSTEM VALUE VALUES (1, 1, '$120.00', '2023-03-27 20:48:02.771323', '2023-03-27 20:48:02.771323', false);
INSERT INTO public."Pozition" ("PozitionID", "PozitionCatID", "PozitionPrice", "PozitionDateAdded", "PozitionDateChanged", "PozitionBought") OVERRIDING SYSTEM VALUE VALUES (2, 2, '$450.00', '2023-03-27 20:48:25.476708', '2023-03-27 20:48:25.476708', false);
INSERT INTO public."Pozition" ("PozitionID", "PozitionCatID", "PozitionPrice", "PozitionDateAdded", "PozitionDateChanged", "PozitionBought") OVERRIDING SYSTEM VALUE VALUES (3, 3, '$200.00', '2023-03-27 20:48:51.127561', '2023-03-27 20:48:51.127561', false);


--
-- TOC entry 2991 (class 0 OID 24842)
-- Dependencies: 201
-- Data for Name: Role; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Role" ("RoleID", "RoleName") OVERRIDING SYSTEM VALUE VALUES (1, 'Клиент');
INSERT INTO public."Role" ("RoleID", "RoleName") OVERRIDING SYSTEM VALUE VALUES (2, 'Админ');


--
-- TOC entry 2993 (class 0 OID 24851)
-- Dependencies: 203
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (1, 'Anton', 'password', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (2, 'User', 'password', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (4, 'Sidorov', '123', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (8, '12345', '12345', 1);
INSERT INTO public."User" ("UserID", "UserLogin", "UserPassword", "UserRoleID") OVERRIDING SYSTEM VALUE VALUES (10, 'admin', '1', 2);


--
-- TOC entry 3007 (class 0 OID 0)
-- Dependencies: 204
-- Name: CatGender_CatGenderID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."CatGender_CatGenderID_seq"', 2, true);


--
-- TOC entry 3008 (class 0 OID 0)
-- Dependencies: 206
-- Name: Cat_catID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Cat_catID_seq"', 3, true);


--
-- TOC entry 3009 (class 0 OID 0)
-- Dependencies: 208
-- Name: Pozition_PozitionID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Pozition_PozitionID_seq"', 3, true);


--
-- TOC entry 3010 (class 0 OID 0)
-- Dependencies: 200
-- Name: Role_RoleID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Role_RoleID_seq"', 8, true);


--
-- TOC entry 3011 (class 0 OID 0)
-- Dependencies: 202
-- Name: User_UserID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_UserID_seq"', 10, true);


--
-- TOC entry 2848 (class 2606 OID 24873)
-- Name: Gender CatGenderPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Gender"
    ADD CONSTRAINT "CatGenderPK" PRIMARY KEY ("GenderID");


--
-- TOC entry 2850 (class 2606 OID 24880)
-- Name: Cat CatPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cat"
    ADD CONSTRAINT "CatPK" PRIMARY KEY ("CatID");


--
-- TOC entry 2852 (class 2606 OID 24911)
-- Name: Pozition PozitionPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pozition"
    ADD CONSTRAINT "PozitionPK" PRIMARY KEY ("PozitionID");


--
-- TOC entry 2854 (class 2606 OID 24913)
-- Name: Pozition PozitionUK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pozition"
    ADD CONSTRAINT "PozitionUK" UNIQUE ("PozitionCatID");


--
-- TOC entry 2840 (class 2606 OID 24846)
-- Name: Role RolePK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Role"
    ADD CONSTRAINT "RolePK" PRIMARY KEY ("RoleID");


--
-- TOC entry 2842 (class 2606 OID 24933)
-- Name: Role RoleUN; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Role"
    ADD CONSTRAINT "RoleUN" UNIQUE ("RoleName");


--
-- TOC entry 2844 (class 2606 OID 24855)
-- Name: User UserPK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "UserPK" PRIMARY KEY ("UserID");


--
-- TOC entry 2846 (class 2606 OID 24857)
-- Name: User UserUK; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "UserUK" UNIQUE ("UserLogin");


--
-- TOC entry 2856 (class 2606 OID 24881)
-- Name: Cat CatGenderFK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cat"
    ADD CONSTRAINT "CatGenderFK" FOREIGN KEY ("CatGenderID") REFERENCES public."Gender"("GenderID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2857 (class 2606 OID 24914)
-- Name: Pozition PozitionCatConstraint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pozition"
    ADD CONSTRAINT "PozitionCatConstraint" FOREIGN KEY ("PozitionCatID") REFERENCES public."Cat"("CatID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2855 (class 2606 OID 24858)
-- Name: User UserRoleConstraint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "UserRoleConstraint" FOREIGN KEY ("UserRoleID") REFERENCES public."Role"("RoleID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 3006 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2023-03-28 22:31:41

--
-- PostgreSQL database dump complete
--

