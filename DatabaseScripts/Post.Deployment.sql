begin try

	begin tran script_postdeployment

	/* 
	 * Configuration data of Board's table 
	 */
	MERGE INTO [dbo].[Board] AS Target 
	USING (VALUES 
		(1, 100)
	)
	AS Source (Id, TotalBoxes) 
	ON Target.Id = Source.Id
	-- update existing rows
	WHEN MATCHED THEN UPDATE SET 
		Id = Source.Id,
		TotalBoxes = Source.TotalBoxes
	-- added new rows
	WHEN NOT MATCHED BY TARGET THEN INSERT (Id, TotalBoxes) 
									VALUES (Id, TotalBoxes) 
	-- delete the records in the target table that are not present in the new values to be synchronized.
	WHEN NOT MATCHED BY SOURCE THEN DELETE;

	/*
	 * Configuration data of SnakeAndLader's table
	 */
	MERGE INTO [dbo].[SnakeAndLader] AS Target 
	USING (VALUES 
		(1, 6, 16, 0),
		(2, 11, 49, 0),
		(3, 25, 46, 0),
		(4, 19, 62, 0),
		(5, 53, 74, 0),
		(6, 60, 64, 0),
		(7, 68, 89, 0),
		(8, 75, 95, 0),
		(9, 80, 99, 0),
		(10, 88, 92, 0),
		(11, 14, 7, 1),
		(12, 26, 15, 1),
		(13, 31, 8, 1),
		(14, 38, 2, 1),
		(15, 42, 21, 1),
		(16, 44, 36, 1),
		(17, 67, 51, 1),
		(18, 84, 28, 1),
		(19, 91, 71, 1),
		(20, 94, 87, 1),
		(21, 98, 78, 1)
	)
	AS Source (Id, StartBox, EndBox, IsLadder) 
	ON Target.Id = Source.Id
	-- update existing rows
	WHEN MATCHED THEN UPDATE SET 
		Id = Source.Id,
		StartBox = Source.StartBox,
		EndBox = Source.EndBox,
		IsLadder = Source.IsLadder
	-- added new rows
	WHEN NOT MATCHED BY TARGET THEN INSERT (Id, StartBox, EndBox, IsLadder) 
									VALUES (Id, StartBox, EndBox, IsLadder) 
	-- delete the records in the target table that are not present in the new values to be synchronized.
	WHEN NOT MATCHED BY SOURCE THEN DELETE;

	/* 
	 * Configuration data of Board's table 
	 */
	MERGE INTO [dbo].[BoardsSnakesAndLaders] AS Target 
	USING (VALUES 
		(1, 1),
		(1, 2),
		(1, 3),
		(1, 4),
		(1, 5),
		(1, 6),
		(1, 7),
		(1, 8),
		(1, 9),
		(1, 10),
		(1, 11),
		(1, 12),
		(1, 13),
		(1, 14),
		(1, 15),
		(1, 16),
		(1, 17),
		(1, 18),
		(1, 19),
		(1, 20),
		(1, 21)
	)
	AS Source (BoardsId, SnakesAndLadersId) 
	ON Target.BoardsId = Source.BoardsId
	-- update existing rows
	WHEN MATCHED THEN UPDATE SET 
		BoardsId = Source.BoardsId,
		SnakesAndLadersId = Source.SnakesAndLadersId
	-- added new rows
	WHEN NOT MATCHED BY TARGET THEN INSERT (BoardsId, SnakesAndLadersId) 
									VALUES (BoardsId, SnakesAndLadersId) 
	-- delete the records in the target table that are not present in the new values to be synchronized.
	WHEN NOT MATCHED BY SOURCE THEN DELETE;

commit tran script_postdeployment

end try

begin catch
	rollback tran script_postdeployment;

	declare @errorMessage nvarchar(4000);
	declare @errorSeverity int;
	declare @errorState int;

	select @errorMessage = ERROR_MESSAGE(), @errorSeverity = ERROR_SEVERITY(), @errorState = ERROR_STATE();		
	raiserror (@errorMessage, @errorSeverity, @errorState);
end catch;
