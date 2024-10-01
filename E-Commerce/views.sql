create or alter view dt_SubCategories as
SELECT s.*,
	   c.title as category_title
  FROM SubCategories s
  left join Categories c on s.category_id=c.id;
GO

create or alter view dt_EndCategories as
SELECT e.*,
      s.title as sub_category_title,
	    c.title as category_title,
      c.id as category_id
  FROM EndCategories e
  left join SubCategories s on e.sub_category_id=s.id
  left join Categories c on s.category_id=c.id;

GO

create or alter view dt_Items as
SELECT i.*,
       e.title as end_category_title,
	   s.id as sub_category_id,
	   s.title as sub_category_title,
	   c.id as category_id,
	   c.title as category_title
  FROM Items i
  left join EndCategories e on i.end_category_id=e.id
  left join SubCategories s on e.sub_category_id=s.id
  left join Categories c on s.category_id=c.id;
