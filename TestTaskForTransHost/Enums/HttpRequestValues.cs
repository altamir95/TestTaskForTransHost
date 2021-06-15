using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskForTransHost.Enums
{ 
    public enum HttpRequestValues
    {
        parametr_is_empty,
        parametr_is_null,
        resource_not_found,
        id_is_less_one,
        date_not_specified,
        room_classes_not_specified,
        client_passport_number_is_null,
        client_passport_number_is_empty,
        client_name_is_null,
        client_name_is_empty,
        client_date_birth_not_specified,
        room_id_is_less_one,
        room_class_id_is_less_one,
        hotel_id_is_less_one,
        room_unexist,
        selected_room_already_reserved,
        unexpected_error_during_reserve,
        unexpected_error,
        enum_room_classes_undefined,
        value_in_enum_room_classes_unexist

    }
}
