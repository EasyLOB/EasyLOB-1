/*
USE Hangfire
GO
*/

TRUNCATE TABLE Hangfire.AggregatedCounter
TRUNCATE TABLE Hangfire.Counter
TRUNCATE TABLE Hangfire.Hash
TRUNCATE TABLE Hangfire.JobParameter
DELETE FROM Hangfire.Job
TRUNCATE TABLE Hangfire.JobQueue
TRUNCATE TABLE Hangfire.List
--TRUNCATE TABLE Hangfire.Schema
TRUNCATE TABLE Hangfire.Server
TRUNCATE TABLE Hangfire.[Set]
TRUNCATE TABLE Hangfire.State
