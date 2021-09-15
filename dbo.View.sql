CREATE VIEW [dbo].[filteredGrundplatten]
	AS SELECT * FROM [Komponente.Grundplatte]
	WHERE Außendurchmesser < 10;
