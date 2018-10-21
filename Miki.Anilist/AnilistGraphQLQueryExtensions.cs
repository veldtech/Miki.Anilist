using Miki.GraphQL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Miki.Anilist
{
	public static class AnilistGraphQLQueryExtensions
	{
		public static TBuilder WithObjects<TBuilder>(this TBuilder b, params string[] objects)
			where TBuilder : IObjectBuilder<TBuilder>
		{
			foreach(var o in objects)
			{
				b.WithObject(o);
			}
			return b;
		}
		
		private static Type GetPropertyOrFieldType(MemberInfo mi)
		{
			switch(mi.MemberType)
			{
				case MemberTypes.Property:
				{
					if((mi as PropertyInfo).CanWrite && (mi as PropertyInfo).CanRead)
					{
						return (mi as PropertyInfo).PropertyType;
					}
				} break;
				case MemberTypes.Field:
				{
					return (mi as FieldInfo).FieldType;
				}
			}
			return null;
		}

		public static TBuilder WithSchema<TBuilder>(this TBuilder b, string schemaName, Type schemaType, Func<ITypeBuilder, ITypeBuilder> predicate)
			where TBuilder : IObjectBuilder<TBuilder>
		{
			return b.WithType(schemaName, x =>
			{
				foreach (var o in schemaType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
				{
					if (o.MemberType != MemberTypes.Property && o.MemberType != MemberTypes.Field)
					{
						continue;
					}

					string name = o.Name;

					var attribute = o.GetCustomAttribute<GraphQLFieldAttribute>(true);
					if (attribute != null)
					{
						name = attribute.Name;
					}

					var iType = GetPropertyOrFieldType(o);

					if (iType == null)
					{
						continue;
					}

					if (iType.GetCustomAttribute<GraphQLSchemaAttribute>() != null)
					{
						x.WithSchema(name, iType, null);
					}
					else
					{
						x.WithObject(name);
					}
				}

				if (predicate != null)
				{
					return predicate(x);
				}
				return x;
			});
		}

		public static TBuilder WithSchema<TBuilder>(this TBuilder b, Type schemaType, Func<ITypeBuilder, ITypeBuilder> predicate)
			where TBuilder : IObjectBuilder<TBuilder>
		{
			var schemaName = schemaType.Name;

			var schema = schemaType.GetCustomAttribute<GraphQLSchemaAttribute>();
			if (schema != null)
			{
				schemaName = schema.Name;
			}

			return WithSchema(b, schemaName, schemaType, predicate);
		}
		public static TBuilder WithSchema<TResult, TBuilder>(this TBuilder b, Func<ITypeBuilder, ITypeBuilder> predicate)
			where TBuilder : IObjectBuilder<TBuilder>
			=> WithSchema(b, typeof(TResult), predicate);
	}
}
